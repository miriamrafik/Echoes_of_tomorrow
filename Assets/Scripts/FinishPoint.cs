using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [Header("Shard Requirement")]
    public int requiredShards = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Block exit if shards are missing
        if (PlayerStats.Shards < requiredShards)
        {
            Debug.Log("Exit locked: Collect all Memory Shards.");
            return; // Aira stays in the level
        }

        // All shards collected → go to next level
        SceneController.instance.NextLevel();
    }
}