using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue5 : MonoBehaviour
{
    public DialogueManager dialogueManager;
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player")
        {
            string[] dialogue={"Archivist Luma: The emotions you healed before are not gone.They return now… unified.Joy, Fear, Anger, and Hope will rise once more stronger, relentless, and unbound. ",
                                "Archivist Luma: Defeat them again, or Kael’s memories will collapse forever.Only then will the Guardian of Memory reveal itself.",
                                "Archivist Luma: This final battle decides everything.Stand firm.Restore Kael’s memories… or lose him entirely."
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