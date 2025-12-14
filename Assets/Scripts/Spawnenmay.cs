using UnityEngine;

public class Spawnenmay: MonoBehaviour
{
    public GameObject dragoPrefab;   // Drago prefab
    public Transform spawnPoint;     // where he appears
    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[DragoTrigger] OnTriggerEnter2D with: " + other.name + " (tag: " + other.tag + ")");

        if (hasSpawned)
        {
            Debug.Log("[DragoTrigger] Already spawned once, ignoring.");
            return;
        }

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[DragoTrigger] Collided object is NOT Player, ignoring.");
            return;
        }

        if (dragoPrefab == null)
        {
            Debug.LogError("[DragoTrigger] dragoPrefab is NOT assigned in Inspector!");
            return;
        }

        Vector3 pos = (spawnPoint != null) ? spawnPoint.position : transform.position;
        GameObject drago = Instantiate(dragoPrefab, pos, Quaternion.identity);
        hasSpawned = true;

        Debug.Log("[DragoTrigger] Spawned Drago at: " + pos + " → instance name: " + drago.name);
    }
}
