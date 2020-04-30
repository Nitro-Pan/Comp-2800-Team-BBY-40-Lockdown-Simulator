using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    public Text touchText;
    public Text boxText;

    private int nActionPointCost;

    void Start() {
        //improve this to draw from a pool, but for now this is okay
        nActionPointCost = Random.Range(1, 11);
    }

    // Update is called once per frame
    void Update() {
        //if (Input.touchCount > 0) {
        //    Touch touch = Input.GetTouch(0);
        //    Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
        //    transform.position = pos;
        //    touchText.text = "TouchX: " + touch.position.x + "\nTouchY: " + touch.position.y;
        //    boxText.text = "BoxX: " + transform.position.x + "\nBoxY: " + transform.position.y
        //        +"\nBoxZ: " + transform.position.z + "\nBoxScaleX: " + transform.localScale.x
        //        +"\nBoxScaleY: " + transform.localScale.y;
        //} else {
        //    touchText.text = "TouchX: Nothing\nTouchY: Nothing";
        //}
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
        }
    }
}
