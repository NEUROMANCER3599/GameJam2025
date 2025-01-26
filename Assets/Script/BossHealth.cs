using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float bossHP;
    public GameObject HitParticlePrefab;
    public GameObject HitSound;
    [SerializeField] private FlashEffect flashEffect;
    private BossBehavior bossSystem;
    private Rigidbody rb;
    private bool IsDead;
    private Scoring scoring;
    private int HitScore;
    private int DeathScore;

    private void Start()
    {
        bossSystem = GetComponent<BossBehavior>();
        rb = GetComponent<Rigidbody>();
        scoring = FindAnyObjectByType<Scoring>();
    }
    public void TakeDamage(int damage)
    {
        flashEffect.Flash(); // Flash Effect
        bossHP -= damage;
        Instantiate(HitSound,transform.position, Quaternion.identity);
        if (bossHP <= 0 && !IsDead)
        {
            Die(); // เรียกฟังก์ชันเมื่อผู้เล่นตาย
        }
    }

    void Die()
    {
        IsDead = true;
       // bossSystem.enabled = false;

    }

    public bool DeathCheck()
    {
        return IsDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            scoring.OnScoring(HitScore);
            TakeDamage(5);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MonsterShooter>())
        {
            MonsterShooter monsterentity = collision.gameObject.GetComponent<MonsterShooter>();
            if (monsterentity.DeathCheck())
            {
                Instantiate(HitParticlePrefab, collision.transform.position, Quaternion.identity);
                scoring.OnScoring(HitScore);
                TakeDamage(1);
            }
        }

        if (collision.gameObject.GetComponent<MonsterChargerr>())
        {
            MonsterChargerr monsterentity = collision.gameObject.GetComponent<MonsterChargerr>();
            if (monsterentity.DeathCheck())
            {
                Instantiate(HitParticlePrefab, collision.transform.position, Quaternion.identity);
                scoring.OnScoring(HitScore);
                TakeDamage(1);
            }
        }

        if (collision.gameObject.GetComponent<FallingItem>())
        {
            FallingItem item = collision.gameObject.GetComponent<FallingItem>();
            if (item.PoppedCheck())
            {
                scoring.OnScoring(HitScore);
                TakeDamage(1);
            }
        }
    }
}
