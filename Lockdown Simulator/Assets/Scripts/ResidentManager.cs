using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResidentManager : MonoBehaviour {

    public float fHappiness;
    private float fTotalHappiness;
    public float fResident;
    private float fTotalResident;
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
        fTotalResident = fResident;
        if (nDay < 1)
            nDay = 1;
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Residents: " + fResident + " / " + fTotalResident;
    }

    public void EndDay() {
        RandomEvent re = RandomEvent.CreateSeededEvent(fHappiness, fResident);
        dm.StartDialogue(re.dialogue);
        nDay += 1;
        fHappiness = Mathf.Clamp(fHappiness += re.fHappinessGain, 0, 100);
        fResident = Mathf.Clamp(fResident += re.fResidentGain, 0, 100);
        if (fResident <= 0)
            EndGame();
        textDay.text = nDay.ToString();
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Residents: " + fResident + " / " + fTotalResident;
    }

    public void UpdateText() {
        textHappiness.text = "Happiness: " + fHappiness + " / " + fTotalHappiness;
        textResident.text = "Residents: " + fResident + " / " + fTotalResident;
    }

    private void EndGame() {
        //TODO end the game somehow
    }
}
