using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DialoguePanel;   // Text (TMP)
    private string[] dialogueSentence;
    private int index = 0;

    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueBox;          // Dialogue Box (Panel)
    public Rigidbody2D playerRB;

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetSentences(string[] sentences)
    {
        dialogueSentence = sentences;
        index = 0;
        DialoguePanel.text = "";
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue());
        
    }

    public IEnumerator TypeDialogue()
    {
        playerRB.constraints =
            RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezePositionY;

        foreach (char letter in dialogueSentence[index].ToCharArray())
        {
            DialoguePanel.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        Debug.Log("Inside NextSentence");
        continueButton.SetActive(false);

        if (index < dialogueSentence.Length - 1)
        {
            index++;
            DialoguePanel.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            DialoguePanel.text = "";
            dialogueBox.SetActive(false);
            dialogueSentence = null;
            index = 0;

            playerRB.constraints = RigidbodyConstraints2D.None;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}