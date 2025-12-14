using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            string[] dialogue={"dvedyfgwygefewyfgw8yfgwyf",
                                "ewfgvewufgwfgwpef",
                                "ewfwfwfcvev",
                                 "ewgvwvewvgwgw"};

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