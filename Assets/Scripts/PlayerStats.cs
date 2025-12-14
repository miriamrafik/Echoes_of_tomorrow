using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class PlayerStats : MonoBehaviour
{
      
    public int maxAetherEnergy = 100; // 4 shards * 25 = 100
    public int aetherEnergy = 0;      // current energy Rocky has
    public static int Shards = 0; // coin 
    public TextMeshProUGUI ShardsCounter; // coin // coin 
    public static int Crystal = 0; // coin 
    public TextMeshProUGUI CrystalCounter;
    public static bool hasHeart = false; // heart part lab 7
  

    public  static int health = 3;
    public static int lives = 3;

    private float flickerTime = 0f;
    public float flickerDuration = 0.1f;

    private SpriteRenderer sr;

    public bool isImmune = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
           // force full bar
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isImmune == true){

            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if(immunityTime >= immunityDuration){

                isImmune = false;
                sr.enabled = true;
            }
        }
             ShardsCounter.text = " " + Shards;
              CrystalCounter.text = " " + Crystal;
    }

    void SpriteFlicker(){

        if(flickerTime < flickerDuration){

            flickerTime = flickerTime + Time.deltaTime;
        }

        else if (flickerTime >= flickerDuration){

            sr.enabled = !(sr.enabled);
            flickerTime = 0;
        }
    }

    public void TakeDamage(int damage){

           if (isImmune == false )
           {
              health = health - damage;
              
              if (health < 0)
              health = 0;

           if (lives > 0 && health == 0){
            FindObjectOfType<LevelManager>().RespawnPlayer();
            health = 3;
           
            lives --;
           }

            else if (lives == 0 && health == 0) {
            Debug.Log("Gameover");
            Destroy(this.gameObject);
            }

            Debug.Log("Player Health:" + health.ToString());
            Debug.Log("Player Lives:" + lives.ToString());

           }

            isImmune = true;
            immunityTime = 0f;

           
    }
    public void GainAetherEnergy(int amount)
    {
        aetherEnergy = aetherEnergy + amount;

        if (aetherEnergy > maxAetherEnergy)
            aetherEnergy = maxAetherEnergy;


    }
   
}