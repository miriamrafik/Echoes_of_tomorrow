using UnityEngine;

public class CollectHeart : MonoBehaviour
{
    public AudioClip heartSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.lives++;   // ✅ correct (instance access)
                AudioManager.Instance.PlayMusicSFX(heartSound);
                Destroy(gameObject);
            }
        }
    }
}
