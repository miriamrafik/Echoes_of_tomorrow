using UnityEngine;
using UnityEngine.SceneManagement; // for scene managment

public class LevelExit : MonoBehaviour
{
    [Header("Shard Requirement")]
    public int requiredShards = 3;

    [Header("Next Level")]
    public string nextSceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to Aira (Player)
        if (!other.CompareTag("Player"))
            return;

        // If shards are NOT enough, stay in current level
        if (PlayerStats.Shards < requiredShards)
        {
            Debug.Log("Exit locked: Collect all 3 Memory Shards.");
            return; // STOP here → Aira stays in the level
            SceneManager.LoadScene("End game");
        }

        // Shards collected → go to next level
        SceneManager.LoadScene(nextSceneName);
    }
}