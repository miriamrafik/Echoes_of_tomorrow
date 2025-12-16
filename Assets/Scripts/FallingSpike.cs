using System.Collections;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallDelay = 0.2f;
    public float fallSpeed = 12f;
    public int damage = 1;

    [Header("Reset Settings")]
    public float resetDelay = 2f;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;

        rb.gravityScale = 0f;
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFallen) return;

        if (other.CompareTag("Player"))
        {
            hasFallen = true;
            Invoke(nameof(StartFalling), fallDelay);
        }
    }

    void StartFalling()
    {
        rb.isKinematic = false;
        rb.gravityScale = fallSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStats stats = collision.collider.GetComponent<PlayerStats>();
            if (stats != null)
                stats.TakeDamage(damage);
        }

        Invoke(nameof(ResetSpike), resetDelay);
    }

    void ResetSpike()
    {
        rb.isKinematic = true;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        transform.position = startPosition;
        hasFallen = false;
    }
}
