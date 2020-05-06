using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    //values of the card itself
    private RandomCardContent content;
    private Color myColor;

    //values for calculating dragging
    public float fReturnSpeed;
    public float fAccelerationCutoff;
    private float fAccelerationY;
    private bool bHeld = false;
    private bool bMoved = false;
    private bool bRerolling = false;
    private Vector3 v3InitialPosition;
    private Vector3 v3OpenPosition;
    private Vector3 v3RerollingPosition;
    private Vector3 v3GrabOffset;
    private Vector3 v3TouchPosition;
    private Vector3 v3LastPosition;

    //Game impact things
    public GameObject gameManagerObject;
    private CardManager cm;
    public Text textApCost;
    public Text textName;
    public Text textDescription;

    void Start() {
        //TODO: improve this to draw from a pool, but for now this is okay
        content = RandomCardContent.GenerateRandomSeededCard(0, 0);
        myColor = GetComponent<SpriteRenderer>().color;
        v3InitialPosition = transform.position;

        v3OpenPosition = v3InitialPosition;
        v3OpenPosition.y += 5;

        v3RerollingPosition = v3InitialPosition;
        v3RerollingPosition.y = -10;

        cm = gameManagerObject.GetComponent<CardManager>();

        textApCost.text = content.nCardCost.ToString();
        textDescription.text = content.sCardContent;
        textName.text = content.sCardName;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(content.sImagePath);
    }

    void Update() {
        if (!bHeld && !bRerolling && (transform.position != v3InitialPosition || transform.position != v3OpenPosition)) {
            //if not held open, return to the initial position
            if (fAccelerationY > fAccelerationCutoff) {
                transform.position = Vector3.Lerp(transform.position, v3OpenPosition, fReturnSpeed * Time.deltaTime);
                v3TouchPosition = v3OpenPosition;
            } else {
                transform.position = Vector3.Lerp(transform.position, v3InitialPosition, fReturnSpeed * Time.deltaTime);
                v3TouchPosition = v3InitialPosition;
            }
        } else if (bHeld) {
            transform.position = v3TouchPosition - v3GrabOffset;
            fAccelerationY = Input.GetTouch(0).deltaPosition.y / Mathf.Pow(Input.GetTouch(0).deltaTime, 2);
        } else if (bRerolling && Mathf.Abs(transform.position.y - v3RerollingPosition.y) < 0.3f) {
            bRerolling = false;
            //TODO: Draw from an event pool
            content = RandomCardContent.GenerateRandomSeededCard(0, 0);
            textApCost.text = content.nCardCost.ToString();
            textDescription.text = content.sCardContent;
            textName.text = content.sCardName;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(content.sImagePath);
            fReturnSpeed = 10.0f;
        } else if (bRerolling) {
            transform.position = Vector3.Lerp(transform.position, v3RerollingPosition, fReturnSpeed * Time.deltaTime);
        }
    }

    public void HitCard(Vector3 v3Pos) {
        v3GrabOffset.x = v3Pos.x - transform.position.x;
        v3GrabOffset.y = v3Pos.y - transform.position.y;
        v3GrabOffset.z = 0;
        v3TouchPosition = v3Pos;
        v3TouchPosition.z = v3InitialPosition.z;

        GetComponent<SpriteRenderer>().color = Color.gray;
        bHeld = true;
    }

    public void ForceDown() {
        if (fAccelerationY > 0 || Mathf.Abs(transform.position.y - v3InitialPosition.y) > 0.1f) {
            fAccelerationY = -1.0f;
            transform.position += Vector3.down * 0.01f;
            bHeld = false;
        }
    }

    public void LiftCard() {
        if (!bMoved)
            if (ClickCard())
                RerollCard();

        GetComponent<SpriteRenderer>().color = myColor;
        bHeld = false;
        bMoved = false;
    }

    public void MoveCard(Vector3 v3Pos) {
        v3LastPosition = transform.position;
        v3TouchPosition = v3Pos;
        v3TouchPosition.z = v3InitialPosition.z;
        bMoved = true;
    }
    

    private bool ClickCard() {
        return cm.UseActionPoints(content.nCardCost);
    }

    public void RerollCard() {
        fAccelerationY = -1.0f;
        fReturnSpeed = 3.0f;
        bRerolling = true;
        bHeld = false;
    }
}
