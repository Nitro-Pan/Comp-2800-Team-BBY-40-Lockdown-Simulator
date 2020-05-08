using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        fHappiness = Mathf.Clamp(fHappiness += re.fHappinessGain, 0, 100);
        fInfectionRate = Mathf.Clamp(fInfectionRate += re.fInfectionRate, 0, 200);
        if (fInfectionRate <= 0)
            EndGame();
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    public void UpdateText() {
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Infected: " + fInfectionRate + "%";
    }

    private void EndGame() {
        //TODO end the game somehow
    }
}
