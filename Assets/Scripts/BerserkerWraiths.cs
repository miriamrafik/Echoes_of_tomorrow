using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerWraiths : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 3;          // bullet hits to kill
    public int contactDamage = 1;      // damage to Rocky on collision

    [Header("Movement")]
    public float moveSpeed = 2f;       // units per second

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    public float attackRange = 3f;     // how far his flamethrower can reach

    [Header("Shard Drop")]
    public GameObject shardPrefab;
    public Transform shardSpawnPoint;

    int currentHealth;
    float attackTimer;
    bool isAggro = false;   // becomes true after first bullet hit
    bool isDead = false;

    bool movingRight = false;   // false = move left first (towards Rocky if he’s on the left)
    float spawnX;
    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    Transform player;       // Rocky

    void Start()
    {
        sr   = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb   = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        spawnX = transform.position.x;

        // Start facing left (towards Rocky in your level layout)
        if (sr != null) sr.flipX = false;
        movingRight = false;

        SetWalking(true);

        // find player once
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (isDead) return;

        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (isDead) {
            if (rb != null) rb.velocity = Vector2.zero;
            return;
        }

        HandleMovement();
    }

    // -------- MOVEMENT: walk left/right, flip on collisions --------
        // -------- MOVEMENT: walk left/right, flip on collisions + stop at spawn --------
    void HandleMovement()
    {
        if (rb == null) return;

        float dir = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);

        // When moving back towards spawn, don't go past spawnX.
        // In your layout Drago starts on the RIGHT side, so spawnX is the RIGHT limit.
        if (movingRight && transform.position.x >= spawnX)
        {
            // clamp exactly to spawn point
            Vector3 pos = transform.position;
            pos.x = spawnX;
            transform.position = pos;

            // turn around and go back towards Rocky again
            FlipDirection();
        }
    }


    void FlipDirection()
    {
        movingRight = !movingRight;

        if (sr != null)
            sr.flipX = !sr.flipX;
    }

    // -------- BULLET DAMAGE (called from PlayerBullet) --------
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        isAggro = true; // first hit makes him angry
        currentHealth -= amount;

        // instantly face Rocky when hit
        if (player != null && sr != null)
        {
            float dx = player.position.x - transform.position.x;
            sr.flipX = dx > 0f;   // face towards Rocky
            // update movingRight to match facing
            movingRight = sr.flipX;
        }

        // If still alive and cooldown ready → retaliate immediately
        if (currentHealth > 0 && attackTimer <= 0f)
        {
            DoRetaliationAttack();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Retaliation attack triggered when hit by bullet
    void DoRetaliationAttack()
    {
        if (player == null) return;

        attackTimer = attackCooldown;

        // play flamethrower anim
        if (anim != null)
            anim.SetTrigger("attack");

        // OPTIONAL: only damage Rocky if he is in front and in range
        Vector2 dirToPlayer = (player.position - transform.position);
        float dist = dirToPlayer.magnitude;

        bool playerOnRight = dirToPlayer.x > 0f;
        bool facingRight = (sr != null && sr.flipX);

        if (dist <= attackRange && (playerOnRight == facingRight))
        {
            PlayerStats stats = player.GetComponent<PlayerStats>();
            if (stats != null)
                stats.TakeDamage(contactDamage);
        }
    }

    void Die()
    {
        isDead = true;

        // stop movement & physics so he doesn't fall
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (anim != null)
        {
            SetWalking(false);
            anim.SetTrigger("die");
        }

        // optional: no more collisions
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Wait 2.2 seconds (death anim) then spawn shard + destroy Drago
        Invoke(nameof(SpawnShardAndDestroy), 1f);
    }

    void SpawnShardAndDestroy()
    {
        SpawnShard();
        Destroy(gameObject);
    }

    void SpawnShard()
    {
        if (shardPrefab == null) return;

        Vector3 pos = (shardSpawnPoint != null)
            ? shardSpawnPoint.position
            : transform.position + Vector3.up;

        Instantiate(shardPrefab, pos, Quaternion.identity);
    }

    void SetWalking(bool walking)
    {
        if (anim == null) return;
        anim.SetBool("isWalking", walking);
    }

    // -------- COLLISION: flip on Player & Wall --------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // optional contact damage – only when aggro if you want
            if (isAggro)
            {
                PlayerStats stats = collision.collider.GetComponent<PlayerStats>();
                if (stats != null)
                    stats.TakeDamage(contactDamage);
            }

            // when he "reaches Rocky", flip and go back
            FlipDirection();
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            // invisible wall at the spawn side → flip and go back toward Rocky
            FlipDirection();
        }
    }
}
