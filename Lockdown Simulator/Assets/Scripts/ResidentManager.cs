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
        dm.StartDialogue(re.dialogue);
        nDay += 1;
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
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    public void UpdateText() {
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    private void EndGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void WinGame() {
        textResident.text = "Game Won!";
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
