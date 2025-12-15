using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public enum CollectibleType { Shard, Crystal }
    public CollectibleType type;
    public AudioClip shardSound;
    public AudioClip crystalSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*
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
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (type == CollectibleType.Shard)
        {
            PlayerStats.Shards++;
            AudioSource.PlayClipAtPoint(shardSound, transform.position);
            Debug.Log("Shards: " + PlayerStats.Shards);
        }
        else if (type == CollectibleType.Crystal)
        {
            PlayerStats.Crystal++;
            AudioSource.PlayClipAtPoint(crystalSound, transform.position);
            Debug.Log("Crystals: " + PlayerStats.Crystal);
        }

        Destroy(gameObject);
    }



}