using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f; // Time before platform starts falling
    public float destroyDelay = 3f; // Time before platform is destroyed
    public float shakeAmount = 0.05f; // How much platform shakes before falling
    
    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool hasTriggered = false;
    private Vector3 originalPosition;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        
        // Make platform static initially
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.gravityScale = 1;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player landed on top of platform
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            // Check if player is above the platform (landed on top)
            if (collision.contacts[0].normal.y < -0.5f)
            {
                hasTriggered = true;
                StartCoroutine(FallSequence());
            }
        }
    }
    
    System.Collections.IEnumerator FallSequence()
    {
        // Shake phase
        float shakeTime = fallDelay;
        float elapsed = 0f;
        
        while (elapsed < shakeTime)
        {
            transform.position = originalPosition + new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0
            );
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // Reset position before falling
        transform.position = originalPosition;
        
        // Start falling
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3; // Falls faster
        }
        
        isFalling = true;
        
        // Destroy after delay
        Destroy(gameObject, destroyDelay);
    }
}