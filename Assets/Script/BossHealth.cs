using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float bossHP;
    [SerializeField] private FlashEffect flashEffect;
    
    
    public void TakeDamage(int damage)
    {
        flashEffect.Flash(); // Flash Effect
        bossHP -= damage;
        if (bossHP <= 0)
        {
            Die(); // เรียกฟังก์ชันเมื่อผู้เล่นตาย
        }
    }

    void Die()
    {
        
    }
}
