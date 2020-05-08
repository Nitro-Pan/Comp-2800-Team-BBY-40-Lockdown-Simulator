using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour {
    //values related to debug
    public Text touchText;
    public Text boxText;

    public float fResidentHappy;
    public int nActionPoints;
    private int nActionPointTotal;
    public Text textActionPoints;

    private GameObject goHitCard;

    //things to see if dialogue is open or not
    public GameObject goDialogueManager;
    private DialogueManager dm;

    // Start is called before the first frame update
    void Start() {
        dm = goDialogueManager.GetComponent<DialogueManager>();
        nActionPointTotal = nActionPoints;
        UseActionPoints(10);
    }

    // Update is called once per frame
    void Update() {
        DetectCardTouch();
    }

    private void DetectCardTouch() {
        //if there is at least one finger on the screen
        if (Input.touchCount > 0 && !dm.bDialogueOpen) {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            //find first frame where the card is touched
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                Collider2D c2DHitObj = Physics2D.Raycast(pos, Camera.main.transform.forward).collider;
                if (c2DHitObj != null && c2DHitObj.CompareTag("Card")) {
                    //hit a card
                    c2DHitObj.gameObject.GetComponent<Card>().HitCard(pos);
                    goHitCard = c2DHitObj.gameObject;
                } else {
                    goHitCard = null;
                }

                //every card that isn't selected will be forced down
                foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card")) {
                    if (card != goHitCard) card.GetComponent<Card>().ForceDown();
                }
            } else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                if (goHitCard != null) {
                    goHitCard.GetComponent<Card>().LiftCard();
                }
            } else if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                if (goHitCard != null) {
                    goHitCard.GetComponent<Card>().MoveCard(pos);
                    boxText.text = "BoxX: " + goHitCard.transform.position.x + "\nBoxY: " + goHitCard.transform.position.y;
                }
            }
        } else {
            touchText.text = "no touch information";
            boxText.text = "no box information";
        }
    }

    public void UseActionPoints(int nUseAP) {
        if (nActionPoints - nUseAP >= 0) {
            nActionPoints -= nUseAP;
            textActionPoints.text = "AP: " + nActionPoints + " / " + nActionPointTotal;
        }
    }
    private void FillActionPoints() {
        nActionPoints = nActionPointTotal;
        textActionPoints.text = "AP: " + nActionPoints + " / " + nActionPointTotal;
    }
    public void ProcessDay() {
        FillActionPoints();
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card")) {
            card.GetComponent<Card>().ForceDown();
        }
        dm.StartDialogue(RandomEvent.CreateEvent().dialogue);
    }
}
