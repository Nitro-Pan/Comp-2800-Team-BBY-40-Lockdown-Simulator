using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //values related to debug
    public Text touchText;
    public Text boxText;

    public float fResidentHappy;

    private GameObject goHitCard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        DetectCardTouch();
    }

    private void DetectCardTouch() {
        //if there is at least one finger on the screen
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
            //find first frame where the card is touched
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                Collider2D c2DHitObj = Physics2D.Raycast(pos, Camera.main.transform.forward).collider;
                if (c2DHitObj != null && c2DHitObj.CompareTag("Card")) {
                    //hit a card
                    c2DHitObj.gameObject.GetComponent<Card>().HitCard(pos);
                }
                goHitCard = c2DHitObj.gameObject;
                //find last frame where the card is touched
            } else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
                foreach (GameObject g in cards) {
                    Card c;
                    if ((c = g.GetComponent<Card>()) != null) {
                        c.LiftCard();
                    }
                }
                //update the card if it is being dragged
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
}
