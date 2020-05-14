using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResidentManager : MonoBehaviour {

    public float fHappiness;
    private float fTotalHappiness;
    public float fInfectionRate;
    private float fTotalInfectionRate;
    [HideInInspector]
    public int nDay = 1;

    public Text textHappiness;
    public Text textResident;
    public Text textDay;

    public GameObject goDialogueManager;
    private DialogueManager dm;

    void Start() {
        dm = goDialogueManager.GetComponent<DialogueManager>();
        fTotalHappiness = fHappiness;
        fTotalInfectionRate = 100;
        if (nDay < 1)
            nDay = 1;
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    public void EndDay() {
        RandomEvent re = RandomEvent.CreateSeededEvent(fHappiness, fInfectionRate);
        fHappiness = Mathf.Clamp(fHappiness + re.fHappinessGain, 0, 100f);
        fInfectionRate = Mathf.Clamp(fInfectionRate + re.fInfectionRate, 0, 100f);
        //if this is above the top, there is no chance of a random event chaning the outcome
        //if it's below, you might get screwed over last minute. Strategy.
        if (fInfectionRate >= fTotalInfectionRate) {
            EndGame();
            return;
        } else if (fInfectionRate <= 0f) {
            WinGame();
            return;
        }
        dm.StartDialogue(re.dialogue);
        nDay += 1;
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    public void UpdateText() {
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    private void EndGame() {
        CanvasGroup ui = GameObject.FindGameObjectWithTag("Game UI").GetComponent<CanvasGroup>();
        StartCoroutine(FadeUI(ui, '-'));
        CreateUI("Screens/Game End");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void WinGame() {
        CanvasGroup ui = GameObject.FindGameObjectWithTag("Game UI").GetComponent<CanvasGroup>();
        StartCoroutine(FadeUI(ui, '-'));
        CreateUI("Screens/Game Win");
    }

    IEnumerator FadeUI(CanvasGroup ui, char op) {
        float fFadeSpeed = 0.05f;
        switch (op) {
            case '-':
                while (ui.alpha > 0) {
                    Debug.Log("Fade UI out");
                    ui.alpha -= fFadeSpeed;
                    yield return null;
                }
                break;
            case '+':
                while (ui.alpha < 1) {
                    Debug.Log("Fade UI in");
                    ui.alpha += fFadeSpeed;
                    yield return null;
                }
                break;
        }
    }

    private void CreateUI(string sFilePath) {
        GameObject goScreen = Resources.Load<GameObject>(sFilePath);
        goScreen.GetComponent<CanvasGroup>().alpha = 0;
        GameObject goEditorObject = Instantiate(goScreen, new Vector3(0, 0, -3), Quaternion.identity);
        StartCoroutine(FadeUI(goEditorObject.GetComponent<CanvasGroup>(), '+'));
    }

    public void IncreaseInfection() {
        fInfectionRate = Mathf.Clamp(fInfectionRate + 10, 0, 100f);
        textResident.text = "Infected: " + fInfectionRate + "%";
    }
    public void DecreaseInfection() {
        fInfectionRate = Mathf.Clamp(fInfectionRate - 10, 0, 100f);
        textResident.text = "Infected: " + fInfectionRate + "%";
    }
}
