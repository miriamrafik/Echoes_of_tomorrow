using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue4 : MonoBehaviour
{
    public DialogueManager dialogueManager;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            string[] dialogue={"Archivist Luma: Beyond this point, there is no retreat.",
                                "Archivist Luma: To restore what was lost… something must be given."
                                 };

            dialogueManager.SetSentences(dialogue);
            Destroy(GetComponent<BoxCollider2D>(),5f);
            }
        }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}