using UnityEngine;
using UnityEngine.UIElements;

public class MonsterChargerr : MonoBehaviour
{
    private PlayerMovement player;
    private Rigidbody2D rb;
    private float moveSpeed;
    public float MinSpeed;
    public float MaxSpeed;
    private bool IsDead = false;
    private bool IsTouched = false;
    private bool IsCharging = false;
    public GameObject HitParticlePrefab;
    private Scoring scoring;
    public int BaseScore = 500;
    public Animator DolphinAnimator;
    public GameObject DeathSound;
    public GameObject AttackSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoring = FindAnyObjectByType<Scoring>();
        player = GameObject.FindAnyObjectByType<PlayerMovement>(); // หา GameObject ที่มี Tag "Player"
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    private void Update()
    {
       
        if (!IsDead)
        {
            ScanningPlayer();
            if (IsTouched)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Death();
                    scoring.OnScoring(BaseScore);
                }
            }

           
            Movement();
            
        }

       
    }

    void ScanningPlayer()
    {
        if (!IsCharging)
        {
            if(transform.position.y > player.transform.position.y)
            {
                ChargePlayer();
            }
            
            if (transform.position.x < player.transform.position.x)
            {
                //moveSpeed = Mathf.Abs(moveSpeed);
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else 
            {
                //moveSpeed = -1 * moveSpeed;
                if(transform.localScale.x > 0)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                }
                
            }
            
        }
        
    }

    void ChargePlayer()
    {
        Instantiate(AttackSound, transform.position, Quaternion.identity);
        DolphinAnimator.SetTrigger("OnAttack");
        IsCharging = true;
        if(transform.position.x < player.transform.position.x)
        {
            moveSpeed = Mathf.Abs(moveSpeed);
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if(transform.position.x > player.transform.position.x)
        {
            moveSpeed = -1 * moveSpeed;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        
    }

    void Movement()
    {
        if (!IsCharging)
        {
            //rb.gravityScale = 0; 
            rb.linearVelocity = new Vector2(0, moveSpeed); 
        }
        else
        {
            //rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocityY + Random.Range(-0.5f,0.15f));
        }


    }

    void Death()
    {
        if (!IsDead)
        {
            
            IsDead = true;
            Instantiate(DeathSound, transform.position, Quaternion.identity);
            rb.gravityScale = 2;
            rb.freezeRotation = false;
            DolphinAnimator.SetTrigger("OnDeath");
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
                //BubbleAnimator.SetBool("IsSteppedOn", true);
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
            //BubbleAnimator.SetBool("IsSteppedOn", false);
            IsTouched = false;
        }
    }



    public bool DeathCheck()
    {
        return IsDead;
    }
}
