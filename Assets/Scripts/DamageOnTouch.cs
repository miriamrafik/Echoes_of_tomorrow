using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 1;
    public bool destroyOnHit = false;   // if you want bullets/enemies to disappear after hitting
    public bool hitOnce = false;        // if true, spikes hit once until player leaves
    private bool hasHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDamage(collision.gameObject);
    }

    void TryDamage(GameObject obj)
    {
        if (hasHit && hitOnce) return;

        PlayerStats player = obj.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(damageAmount);
            hasHit = true;

            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
