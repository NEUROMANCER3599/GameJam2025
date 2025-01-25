
using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // พรีแฟบของ projectile
    public Transform firePoint; // จุดยิง projectile
    public float fireInterval = 2f; // ระยะเวลาระหว่างการยิง
    public float projectileSpeed = 5f; // ความเร็วของ projectile
    public Animator BubbleAnimator;
    public GameObject BubbleSprite;

    private PlayerMovement player; // อ้างอิงตำแหน่งของผู้เล่น
    private Rigidbody2D rb;
    private float moveSpeed;
    private bool IsDead = false;
    private bool IsTouched = false;
    void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerMovement>(); // หา GameObject ที่มี Tag "Player"
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ShootProjectile", 1f, fireInterval); // ยิง projectile เป็นระยะ
        moveSpeed = Random.Range(10, 15);
    }

    private void Update()
    {
        Movement();


        if (IsTouched)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Death();
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
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // คำนวณทิศทางไปยังผู้เล่น
            Vector2 direction = (player.transform.position - firePoint.position).normalized;

            // เพิ่ม Rigidbody2D ให้ projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed; // กำหนดความเร็วและทิศทางของ projectile
            }
        }
        // สร้าง projectile
       
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
