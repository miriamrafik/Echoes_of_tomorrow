using UnityEngine;

public class ShardPickup : MonoBehaviour
{
    [Header("Aether")]
    [Tooltip("How much Aether this shard gives.")]
    public int aetherAmount = 25;

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats == null)
            return;

        // 1) Give Aether energy
        stats.GainAetherEnergy(aetherAmount);

        // 2) Unlock the correct ability (or multiple if you check more than one)

        // 3) Remove the shard from the scene
        PlayerStats.Crystal++;

        Destroy(gameObject);
    }
}
