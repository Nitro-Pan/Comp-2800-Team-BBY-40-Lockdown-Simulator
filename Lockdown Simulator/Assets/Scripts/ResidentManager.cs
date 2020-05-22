using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResidentManager : MonoBehaviour {

    public float fHappiness;
    private float fTotalHappiness;
    private float fOverallHappiness;
    public float fInfectionRate;
    private float fTotalInfectionRate;
    [HideInInspector]
    public int nDay = 1;

    public Text textHappiness;
    public Text textResident;
    public Text textDay;

    public GameObject goDialogueManager;
    private DialogueManager dm;

    //leaderboard
    public LeaderboardManager lm;

    void Start() {
        dm = goDialogueManager.GetComponent<DialogueManager>();
        fTotalHappiness = 100;
        fTotalInfectionRate = 100;
        if (nDay < 1)
            nDay = 1;
        textDay.text = nDay.ToString();
        textHappiness.text = fHappiness.ToString("0.#") + "%";
        textResident.text = fInfectionRate.ToString("0.#") + "%";
    }

    public void EndDay() {
        RandomEvent re = RandomEvent.CreateSeededEvent(fHappiness, fInfectionRate);
        fHappiness = Mathf.Clamp(fHappiness + re.fHappinessGain, 0, 100f);
        fInfectionRate = Mathf.Clamp(fInfectionRate + re.fInfectionRate, 0, 100f);
        //if this is above the top, there is no chance of a random event chaning the outcome
        //if it's below, you might get screwed over last minute. Strategy.
        dm.StartDialogue(re.dialogue);
        fOverallHappiness += fHappiness;
        if (fInfectionRate >= fTotalInfectionRate) {
            EndGame();
            return;
        } else if (fInfectionRate <= 0f) {
            WinGame();
            return;
        }
        nDay += 1;
        textDay.text = nDay.ToString();
        textHappiness.text = fHappiness.ToString("0.#") + "%";
        textResident.text = fInfectionRate.ToString("0.#") + "%";
    }

    public void UpdateText() {
        textHappiness.text = fHappiness.ToString("0.#") + "%";
        textResident.text = fInfectionRate.ToString("0.#") + "%";
    }

    private void EndGame() {
        CanvasGroup ui = GameObject.FindGameObjectWithTag("Game UI").GetComponent<CanvasGroup>();
        StartCoroutine(FadeUI(ui, '-'));
        CreateUI("Screens/Game End");
    }
    private void WinGame() {
        CanvasGroup ui = GameObject.FindGameObjectWithTag("Game UI").GetComponent<CanvasGroup>();
        StartCoroutine(FadeUI(ui, '-'));
        CreateUI("Screens/Game Win");
        lm.SaveScore((fOverallHappiness / nDay) * (5f / nDay));
        Debug.Log($"Sending score with on day {nDay} average happiness {fOverallHappiness / nDay}, at over happiness {fOverallHappiness} at a score constant of {5f / nDay}");
    }

    IEnumerator FadeUI(CanvasGroup ui, char op) {
        while (dm.bDialogueOpen) {
            yield return null;
        }
        ui.blocksRaycasts = true;
        ui.interactable = true;
        float fFadeSpeed = 0.05f;
        switch (op) {
            case '-':
                while (ui.alpha > 0) {
                    ui.alpha -= fFadeSpeed;
                    yield return null;
                }
                break;
            case '+':
                while (ui.alpha < 1) {
                    ui.alpha += fFadeSpeed;
                    yield return null;
                }
                break;
        }
    }

    private void CreateUI(string sFilePath) {
        GameObject goScreen = Resources.Load<GameObject>(sFilePath);
        //this assumes that everything is set up properly
        goScreen.transform.GetChild(3).GetComponent<Text>().text = $"It took you {nDay} day" + ((nDay & 1) == 1 ? "" : "s");
        goScreen.transform.GetChild(4).GetComponent<Text>().text = "With an average happiness of " + (fOverallHappiness / nDay).ToString("0.#");
        CanvasGroup cg = goScreen.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
        Canvas c = goScreen.GetComponent<Canvas>();
        c.worldCamera = Camera.main;
        c.planeDistance = 5;
        GameObject goEditorObject = Instantiate(goScreen, new Vector3(0, 0, -3), Quaternion.identity);
        StartCoroutine(FadeUI(goEditorObject.GetComponent<CanvasGroup>(), '+'));
    }

    public void IncreaseInfection() {
        fInfectionRate = Mathf.Clamp(fInfectionRate + 10, 0, 100f);
        textResident.text = fInfectionRate.ToString("0.#") + "%";
    }
    public void DecreaseInfection() {
        fInfectionRate = Mathf.Clamp(fInfectionRate - 10, 0, 100f);
        textResident.text = fInfectionRate.ToString("0.#") + "%";
    }
}
