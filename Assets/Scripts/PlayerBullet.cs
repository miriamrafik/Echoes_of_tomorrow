using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    public ABShooting player;          // script that fires the bullet
    private SpriteRenderer sr;         // sprite on the bullet itself
    private Rigidbody2D rb;            // rigidbody of the bullet

    private float timer = 0f;
    private float lifeTime = 2f;

    void Start()
    {
        // find the shooter (Rocky) and components on the bullet
        player = FindObjectOfType<ABShooting>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // make bullet face same direction as the player sprite
        if (player != null && player.sr != null && sr != null)
        {
            sr.flipX = player.sr.flipX;
        }
    }

    void Update()
    {
        if (sr.flipX)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        // lifetime
        if ((timer += Time.deltaTime) >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Drago drago = other.GetComponent<Drago>();
        if (drago != null)
        {
            drago.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }


    }


    
}
