using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        
          if (other.tag == "Player"){
        
            PlayerStats.Shards++;
            Debug.Log("Shards: " + PlayerStats.Shards);
            
        }
         else if (other.tag == "Player"){
        
            PlayerStats.Crystal++;

            Debug.Log("Crystals: " + PlayerStats.Crystal);
            }
           
        Destroy(gameObject);
    }

}