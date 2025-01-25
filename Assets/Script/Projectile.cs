using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // ความเสียหายที่ projectile สร้าง

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

      //  if (collision.CompareTag("Ground")) // ทำลาย projectile เมื่อชนพื้น
       // {
     //       Destroy(gameObject);
      //  }
    }

    private void Update()
    {
        Destroy(gameObject,5f);
    }
}
