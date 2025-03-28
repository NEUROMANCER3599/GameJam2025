using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // พรีแฟบของ projectile
    public Transform firePoint; // จุดยิง projectile
    public float fireInterval = 2f; // ระยะเวลาระหว่างการยิง
    public float MinSpeed;
    public float MaxSpeed;
    public Animator BubbleAnimator;
    public Animator StarfishAnimator;
    public GameObject BubbleSprite;
    public GameObject StarfishSprite;
    public GameObject HitParticlePrefab;
    private Transform playerTransform;
    private PlayerMovement player; // อ้างอิงตำแหน่งของผู้เล่น
    private Rigidbody2D rb;
    private float moveSpeed;
    private bool IsDead = false;
    private bool IsTouched = false;
    private Scoring scoring;
    public int BaseScore = 500;
    public GameObject DeathSound;
    public GameObject AttackSound;
    void Start()
    {
        // หา player โดยใช้ Tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }


        scoring = FindAnyObjectByType<Scoring>();
        player = GameObject.FindAnyObjectByType<PlayerMovement>(); // หา GameObject ที่มี Tag "Player"
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ShootProjectile", 1f, fireInterval); // ยิง projectile เป็นระยะ
        moveSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    private void Update()
    {
        Movement();

        if (!IsDead)
        {
            if (IsTouched)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Death();
                    scoring.OnScoring(BaseScore);
                }
            }

            if (transform.position.x < player.transform.position.x)
            {
                //moveSpeed = Mathf.Abs(moveSpeed);
                StarfishSprite.transform.localScale = new Vector2(Mathf.Abs(StarfishSprite.transform.localScale.x), StarfishSprite.transform.localScale.y);
            }
            else
            {
                //moveSpeed = -1 * moveSpeed;
                if (StarfishSprite.transform.localScale.x > 0)
                {
                    StarfishSprite.transform.localScale = new Vector2(-StarfishSprite.transform.localScale.x, StarfishSprite.transform.localScale.y);
                }

            }
        }

    }

    void Movement()
    {
        if (!IsDead)
        {
            rb.gravityScale = 0; // ปิดแรงโน้มถ่วง
            rb.linearVelocity = new Vector2(0, moveSpeed); // กำหนดให้วัตถุเลื่อนขึ้นในแนว Y
        }

    }

    void Death()
    {
        if (!IsDead)
        {
            IsDead = true;
            gameObject.layer = 13;
            Instantiate(DeathSound, transform.position, Quaternion.identity);
            BubbleAnimator.SetTrigger("OnDeath");
            StarfishAnimator.SetTrigger("OnDeath");
            rb.gravityScale = 1;
            rb.freezeRotation = false;
            Invoke(nameof(DisableSprite), 1f);
        }
       

    }

    void DisableSprite()
    {
        BubbleSprite.SetActive(false);
    }

    void ShootProjectile()
    {
        if (player == null) return;

        if (!IsDead)
        {
            if (playerTransform.position.y < firePoint.transform.position.y)
            {
                Instantiate(AttackSound, transform.position, Quaternion.identity);
                StarfishAnimator.SetTrigger("Attacking");
                // สร้าง projectile
                Debug.Log("Player is below");
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cleaner")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Explosion")
        {
            scoring.OnScoring(BaseScore);
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (!IsTouched)
            {
                BubbleAnimator.SetBool("IsSteppedOn", true);
                IsTouched = true;
            }

        }
        if (collision.gameObject.GetComponent<MonsterShooter>())
        {
            MonsterShooter monsterentity = collision.gameObject.GetComponent<MonsterShooter>();
            if (monsterentity.DeathCheck())
            {
                Instantiate(HitParticlePrefab, collision.transform.position, Quaternion.identity);
                scoring.OnScoring(BaseScore);
                Death();
            }
        }

        if (collision.gameObject.GetComponent<MonsterChargerr>())
        {
            MonsterChargerr monsterentity = collision.gameObject.GetComponent<MonsterChargerr>();
            if (monsterentity.DeathCheck())
            {
                Instantiate(HitParticlePrefab, collision.transform.position, Quaternion.identity);
                scoring.OnScoring(BaseScore);
                Death();
            }
        }

        if (collision.gameObject.GetComponent<FallingItem>())
        {
            FallingItem item = collision.gameObject.GetComponent<FallingItem>();
            if (item.PoppedCheck())
            {
                scoring.OnScoring(BaseScore);
                Death();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            BubbleAnimator.SetBool("IsSteppedOn", false);
            IsTouched = false;
        }
    }

    public bool DeathCheck()
    {
        return IsDead;
    }
}
