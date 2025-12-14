using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float destroyDelay = 3f;
    public float respawnDelay = 2f;
    public float shakeAmount = 0.05f;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer sr;

    private bool hasTriggered = false;
    private Vector3 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        originalPosition = transform.position;

        ResetPlatform();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                hasTriggered = true;
                StartCoroutine(FallSequence());
            }
        }
    }

    IEnumerator FallSequence()
    {
        // Shake before falling
        float elapsed = 0f;

        while (elapsed < fallDelay)
        {
            transform.position = originalPosition + new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        // Start falling
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;

        yield return new WaitForSeconds(destroyDelay);

        // Hide platform instead of destroying
        sr.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        Respawn();
    }

    void Respawn()
    {
        transform.position = originalPosition;
        ResetPlatform();
    }

    void ResetPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Static;
        rb.gravityScale = 1f;

        sr.enabled = true;
        col.enabled = true;

        hasTriggered = false;
    }
}
