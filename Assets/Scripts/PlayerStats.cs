using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // for scene manager
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
    public Image healthBar; //healthBar

    public  static int health = 3;
    public static int lives = 10;
    public int maxHealth = 3; //healthBar

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
        health = maxHealth; //healthBar
        UpdateHealthBar(); //healthBar
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
             PlayerStats.health -= damage;   //healthBar
             PlayerStats.health = Mathf.Clamp(PlayerStats.health, 0, 3);  //healthBar
             UpdateHealthBar();  //healthBar


           if (lives > 0 && health == 0){
            FindObjectOfType<LevelManager>().RespawnPlayer();
            health = 3;
            UpdateHealthBar(); //healthBar
            lives --;
           }

            else if (lives == 0 && health == 0) {
                
                Debug.Log("Game Over");

                // Reset stats
                lives = 10;
                health = maxHealth;
                UpdateHealthBar();

                // Load Game Over scene OR directly reload last level
                SceneManager.LoadScene("End game");
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
    
      void UpdateHealthBar()
    {
        //READ static health
        healthBar.fillAmount = (float)PlayerStats.health / maxHealth;
    }
   
}