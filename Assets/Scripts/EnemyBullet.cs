// EnemyBullet.cs
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 3f;
    public float speed = 5f;    // سرعة الرصاصة
    [HideInInspector] public Vector2 direction;  // الاتجاه الذي تتحرك فيه الرصاصة

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
                stats.TakeDamage(damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
