using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : EnemyController // ex: the chanisaw they showed us in the lab 
{

    public Transform player; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.deltaTime);
    }

    /*override the OnTrigger() function and delete the flip  
    function call since we do not want the saw to flip and turn away if it 
    hits the player, only the wall.*/
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
        else if (other.tag == "Wall")
        {
            Flip();
        }
    }

}

/*
In unity make speed = 3, damage = 1, player (drage and drop booby)
*/
