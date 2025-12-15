using UnityEngine;

public class Guardian : MonoBehaviour
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

    bool movingRight = false;   // false = move left first
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

        if (sr != null) sr.flipX = false;
        movingRight = false;

        SetWalking(true);

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
        if (isDead)
        {
            if (rb != null) rb.velocity = Vector2.zero;
            return;
        }

        HandleMovement();
    }

    void HandleMovement()
    {
        if (rb == null) return;

        float dir = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);

        if (movingRight && transform.position.x >= spawnX)
        {
            Vector3 pos = transform.position;
            pos.x = spawnX;
            transform.position = pos;

            FlipDirection();
        }
    }

    void FlipDirection()
    {
        movingRight = !movingRight;

        if (sr != null)
            sr.flipX = !sr.flipX;
    }

    // -------- BULLET DAMAGE --------
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        isAggro = true;
        currentHealth -= amount;

        if (player != null && sr != null)
        {
            float dx = player.position.x - transform.position.x;
            sr.flipX = dx > 0f;
            movingRight = sr.flipX;
        }

        if (currentHealth > 0 && attackTimer <= 0f)
        {
            DoRetaliationAttack();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void DoRetaliationAttack()
    {
        if (player == null) return;

        attackTimer = attackCooldown;

        if (anim != null)
            anim.SetTrigger("attack");

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

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (isAggro)
            {
                PlayerStats stats = collision.collider.GetComponent<PlayerStats>();
                if (stats != null)
                    stats.TakeDamage(contactDamage);
            }

            FlipDirection();
        }
    }
}
