using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public bool bDialogueOpen;

    private Queue<string> sentences;

    //used so that if the user clicks too fast the coroutine can be stopped
    //and started again without conflicting.
    private Coroutine sentenceBox;

    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        if (!bDialogueOpen) {
            animator.SetBool("isOpen", true);
            bDialogueOpen = true;

            nameText.text = dialogue.sName;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences) {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        if (sentenceBox != null)
            StopCoroutine(sentenceBox);

        sentenceBox = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray()) {
            dialogueText.text += c;
            //wait for one frame before running the loop again
            yield return null;
        }
    }

    public void EndDialogue() {
        animator.SetBool("isOpen", false);
        bDialogueOpen = false;
        sentenceBox = null;
    }
}
