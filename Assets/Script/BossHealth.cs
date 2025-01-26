using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float bossHP;
    public GameObject HitParticlePrefab;
    public GameObject HitSound;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private Animator BossAnimator;
    [SerializeField] private SpriteRenderer BossSprite;
    [SerializeField] private GameObject MinaProp;
    [SerializeField] private GameObject FinalExplosion;
    private BossBehavior bossSystem;
    private Rigidbody2D rb;
    private bool IsDead;
    private Scoring scoring;
    [SerializeField] private int HitScore;
    [SerializeField] private int DeathScore;
    private PlayerMovement player;
    private PlayerHealth playerHealth;
    private PlayerFalling playerFalling;
    private float WinTimer = 3f;
    private UIControl UISystem;


    private void Start()
    {
        bossSystem = GetComponent<BossBehavior>();
        rb = GetComponent<Rigidbody2D>();
        scoring = FindAnyObjectByType<Scoring>();
        player = FindAnyObjectByType<PlayerMovement>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        playerFalling = FindAnyObjectByType<PlayerFalling>();
        UISystem = FindAnyObjectByType<UIControl>();
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
    private void FixedUpdate()
    {
        if (!IsDead)
        {
            if (transform.position.x < player.transform.position.x)
            {
                //moveSpeed = Mathf.Abs(moveSpeed);
                BossSprite.flipX = false;
            }
            else
            {
                BossSprite.flipX = true;

            }
        }
    }

    void Die()
    {
        IsDead = true;
        BossAnimator.SetBool("IsDead", true);
        bossSystem.enabled = false;
        rb.gravityScale = 0.5f;
        Instantiate(FinalExplosion,transform.position, Quaternion.identity);
        Instantiate(MinaProp, transform.position, Quaternion.identity);
        playerHealth.OnWin();
        playerFalling.OnWin();
        Invoke(nameof(GameWin), WinTimer);

        // bossSystem.enabled = false;

    }

    void GameWin()
    {
        // player.gameObject.SetActive(false);
        Time.timeScale = 0;
        UISystem.OnWin();
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
