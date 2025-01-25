using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private FlashEffect flashEffect;
    public int maxHealth = 100; // ค่าพลังชีวิตสูงสุด
    private int currentHealth;
    private bool isFlashing;
    public float flashCooldown = 0f; // ตัวแปรสำหรับจับเวลา
    public GameObject gameOverUI;
    private bool IsDead = false;
    void Start()
    {
        currentHealth = maxHealth; // ตั้งค่าพลังชีวิตเริ่มต้น
    }

    private void Update()
    {
        if (isFlashing && Time.time >= flashCooldown)
        {
            isFlashing = false; // รีเซ็ตสถานะ isFlashing
        }
    }

    public void TakeDamage(int damage)
    {
        if (isFlashing != true)
        {
            flashEffect.Flash(); // Flash Effect
            currentHealth -= damage; // ลดพลังชีวิตตามความเสียหาย
            Debug.Log("Player Health: " + currentHealth);
            isFlashing = true;
            flashCooldown = Time.time + 1f;
        }
       

        if (currentHealth <= 0)
        {
            Die(); // เรียกฟังก์ชันเมื่อผู้เล่นตาย
        }
    }

    public void Die()
    {
        IsDead = true;
        gameObject.layer = 10;
        Invoke(nameof(SpawnGameOverUI), 3f);
        Debug.Log("Player is dead!");
        // เพิ่มโค้ดเพิ่มเติมเมื่อผู้เล่นตาย เช่น รีเซ็ตเกม หรือแสดง Game Over
    }

    public void SpawnGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public bool PlayerDeathCheck()
    {
        return IsDead;
    }
}
