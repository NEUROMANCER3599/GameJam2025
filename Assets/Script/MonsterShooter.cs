using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // พรีแฟบของ projectile
    public Transform firePoint; // จุดยิง projectile
    public float fireInterval = 2f; // ระยะเวลาระหว่างการยิง
    public float projectileSpeed = 5f; // ความเร็วของ projectile

    private Transform player; // อ้างอิงตำแหน่งของผู้เล่น

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // หา GameObject ที่มี Tag "Player"
        InvokeRepeating("ShootProjectile", 1f, fireInterval); // ยิง projectile เป็นระยะ
    }

    void ShootProjectile()
    {
        if (player == null) return;

        // สร้าง projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // คำนวณทิศทางไปยังผู้เล่น
        Vector2 direction = (player.position - firePoint.position).normalized;

        // เพิ่ม Rigidbody2D ให้ projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed; // กำหนดความเร็วและทิศทางของ projectile
        }
    }
}
