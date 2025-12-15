using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue2 : MonoBehaviour
{
    public DialogueManager dialogueManager;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            string[] dialogue={"Archivist Luma: Fear listens.",
                                "Archivist Luma: Light and sound will draw what hides in the dark."
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