using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            string[] dialogue={"Archivist Luma: You are within a fading memory.You must move carefully.Use the Left and Right Arrow keys to walk.",
                                "Archivist Luma: Memories are unstable. You will need to move upward.Press Space to jump.",
                                 "Archivist Luma:That shadow is an echo of yourself. It will follow what you do. Light is the only thing it fears.Press Q to release a Light Burst.",
                                "Archivist Luma:This memory is fading. Some paths reveal themselves only when illuminated.Gather what remains before the joy disappears completely. ",
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