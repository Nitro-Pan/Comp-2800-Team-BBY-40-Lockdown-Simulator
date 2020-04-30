using UnityEngine;

public class Card : MonoBehaviour {
    //values of the card itself
    private int nActionPointCost;
    private Color myColor;

    //values for calculating dragging
    public float fReturnSpeed;
    private bool bHeld = false;
    private Vector3 v3InitialPosition;
    private Vector3 v3GrabOffset;
    private Vector3 v3TouchPosition;

    void Start() {
        //improve this to draw from a pool, but for now this is okay
        nActionPointCost = Random.Range(1, 11);
        myColor = GetComponent<SpriteRenderer>().color;
        v3InitialPosition = transform.position;
    }

    void Update() {
        if (!bHeld && transform.position != v3InitialPosition) {
            //if not held, Lerp back to the initial position
            transform.position = Vector3.Lerp(transform.position, v3InitialPosition, fReturnSpeed * Time.deltaTime);
            v3TouchPosition = v3InitialPosition;
        } else if (bHeld) {
            transform.position = v3TouchPosition - v3GrabOffset;
        }
    }

    public void HitCard(Vector3 v3Pos) {
        Debug.Log("Hit card with cost " + nActionPointCost);

        v3GrabOffset.x = v3Pos.x - transform.position.x;
        v3GrabOffset.y = v3Pos.y - transform.position.y;
        v3GrabOffset.z = 0;
        v3TouchPosition = v3Pos;
        v3TouchPosition.z = v3InitialPosition.z;

        GetComponent<SpriteRenderer>().color = Color.gray;
        bHeld = true;
    }
    public void LiftCard() {
        Debug.Log("Hit card with cost " + nActionPointCost);

        GetComponent<SpriteRenderer>().color = myColor;
        bHeld = false;
    }
    public void MoveCard(Vector3 v3Pos) {
        v3TouchPosition = v3Pos;
        v3TouchPosition.z = v3InitialPosition.z;
    }
}
