using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // ค่าพลังชีวิตสูงสุด
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // ตั้งค่าพลังชีวิตเริ่มต้น
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ลดพลังชีวิตตามความเสียหาย
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // เรียกฟังก์ชันเมื่อผู้เล่นตาย
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        // เพิ่มโค้ดเพิ่มเติมเมื่อผู้เล่นตาย เช่น รีเซ็ตเกม หรือแสดง Game Over
    }
}
