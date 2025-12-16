using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABShooting : MonoBehaviour
{
    [Header("References")]
    public Transform shootingPoint;          // where bullets spawn
    public GameObject normalBulletPrefab;    // old bullet prefab (with PlayerBullet)
    public SpriteRenderer sr;                // Rocky's SpriteRenderer

    private PlayerStats playerStats;         // we’ll grab it in Awake

    [Header("Keys")]
    public KeyCode normalKey = KeyCode.Tab;  // normal shot

    [Header("Cooldowns")]
    public float normalCooldown = 0.25f;

    private float normalTimer = 0f;

    void Awake()
    {
        // Make sure we are using the same PlayerStats that gets the shard
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats == null)
            Debug.LogWarning("[ABShooting] No PlayerStats found in scene!");
    }

    void Start()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        normalTimer -= Time.deltaTime;

        // 🔫 Normal bullet (Q)
        if (Input.GetKeyDown(normalKey) && normalTimer <= 0f)
        {
            ShootNormal();
            normalTimer = normalCooldown;
        }


    }

    void ShootNormal()
    {
        if (normalBulletPrefab == null || shootingPoint == null)
        {
            Debug.LogWarning("[ABShooting] Normal bullet prefab or shootingPoint missing!");
            return;
        }

        Instantiate(normalBulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }

    
}
