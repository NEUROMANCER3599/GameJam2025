using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // พรีแฟบของ projectile
    public Transform firePoint; // จุดยิง projectile
    public float fireInterval = 2f; // ระยะเวลาระหว่างการยิง
    public Animator BubbleAnimator;
    public GameObject BubbleSprite;
    private Transform playerTransform;
    private PlayerMovement player; // อ้างอิงตำแหน่งของผู้เล่น
    private Rigidbody2D rb;
    private float moveSpeed;
    private bool IsDead = false;
    private bool IsTouched = false;
    private Scoring scoring;
    public int BaseScore = 500;
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
        moveSpeed = Random.Range(15, 20);
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
        IsDead = true;
        BubbleAnimator.SetTrigger("OnDeath");
        rb.gravityScale = 1;
        rb.freezeRotation = false;
        Invoke(nameof(DisableSprite), 1f);

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
                scoring.OnScoring(BaseScore);
                Death();
            }
        }

        if (collision.gameObject.GetComponent<MonsterChargerr>())
        {
            MonsterChargerr monsterentity = collision.gameObject.GetComponent<MonsterChargerr>();
            if (monsterentity.DeathCheck())
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
