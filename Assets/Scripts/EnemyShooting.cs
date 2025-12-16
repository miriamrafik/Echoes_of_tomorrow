// EnemyShooting.cs
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("References")]
    public Transform shootingPoint;      // نقطة إطلاق الرصاص
    public GameObject bulletPrefab;      // الرصاصة
    public Rigidbody2D rb;               // Rigidbody للعدو لمعرفة الحركة

    [Header("Cooldowns")]
    public float shootCooldown = 1f;     // كل ثانية يطلق رصاصة
    private float shootTimer = 0f;

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootingPoint == null || rb == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        // حساب الاتجاه بناءً على حركة العدو
        Vector2 direction = rb.velocity.normalized;  // اتجاه حركة العدو
        if (direction == Vector2.zero)
            direction = Vector2.right; // افتراضي إذا العدو ساكن

        bullet.transform.right = direction; // تدوير الرصاصة لتتجه بنفس الاتجاه

        // تمرير الاتجاه إلى سكريبت الرصاصة
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {
            bulletScript.direction = direction;
            bulletScript.speed = 5f; // سرعة الرصاصة
        }
    }
}
