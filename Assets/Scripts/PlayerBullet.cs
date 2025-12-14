using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    public ABShooting player;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private float timer = 0f;
    private float lifeTime = 2f;

    void Start()
    {
        player = FindObjectOfType<ABShooting>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (player != null && player.sr != null && sr != null)
            sr.flipX = player.sr.flipX;
    }

    void Update()
    {
        rb.velocity = sr.flipX
            ? new Vector2(-speed, rb.velocity.y)
            : new Vector2(speed, rb.velocity.y);

        if ((timer += Time.deltaTime) >= lifeTime)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Drago drago = other.GetComponent<Drago>();
        if (drago != null)
        {
            drago.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Spider spider = other.GetComponent<Spider>();
        if (spider != null)
        {
            spider.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        BerserkerWraiths wraith = other.GetComponent<BerserkerWraiths>();
        if (wraith != null)
        {
            wraith.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }
}
