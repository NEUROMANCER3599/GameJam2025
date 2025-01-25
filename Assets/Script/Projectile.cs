using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // ความเสียหายที่ projectile สร้าง
    public float speed = 5f; // ความเร็วของ projectile
    private Transform player; // อ้างอิงตำแหน่งของผู้เล่น
    private Vector2 targetDirection; // ทิศทางเป้าหมาย

    void Start()
    {
        // หา player โดยใช้ Tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;

            // คำนวณทิศทางไปยังผู้เล่น
            targetDirection = (player.position - transform.position).normalized;

            // ปรับการหมุนให้ projectile หันไปในทิศทางของผู้เล่น
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void Update()
    {
        // เคลื่อนที่ไปในทิศทางที่กำหนด
        transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);

        // ทำลาย projectile หลังจาก 5 วินาที
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ตรวจสอบว่าชนผู้เล่นหรือไม่
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // ลดค่าพลังชีวิตของผู้เล่น
            }

            Destroy(gameObject); // ทำลาย projectile หลังชน
        }
    }
}
