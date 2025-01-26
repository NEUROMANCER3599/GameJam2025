using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public float MinSpeed;
    public float MaxSpeed;
    public Animator BubbleAnimator;
    public Animator ItemAnimator;
    public GameObject BubbleSprite;
    public GameObject ItemSprite;
    public GameObject HitParticlePrefab;
    private Rigidbody2D rb;
    private float moveSpeed;
    private bool IsPopped = false;
    private bool IsTouched = false;
    private Scoring scoring;
    public int BaseScore = 500;
    public bool DestroyItemOnHit = false;
    public GameObject PopSound;
    public GameObject HitSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoring = FindAnyObjectByType<Scoring>();
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(MinSpeed, MaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
            Movement();

            if (!IsPopped)
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
        if (!IsPopped)
        {
            rb.gravityScale = 0; 
            rb.linearVelocity = new Vector2(0, moveSpeed); 
        }
    }

    void Death()
    {
        if (!IsPopped)
        {
            
            IsPopped = true;
            Instantiate(PopSound, transform.position, Quaternion.identity);
            BubbleAnimator.SetTrigger("OnDeath");
            //StarfishAnimator.SetTrigger("OnDeath");
            rb.gravityScale = 1;
            rb.freezeRotation = false;
            Invoke(nameof(DisableSprite), 1f);
        }
      
    }

    void DisableSprite()
    {
        BubbleSprite.SetActive(false);
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
            if (IsPopped && !collision.gameObject.GetComponent<MonsterShooter>().DeathCheck())
            {
                Instantiate(HitParticlePrefab, transform.position, Quaternion.identity);
                Instantiate(HitSound, transform.position, Quaternion.identity);
                if (DestroyItemOnHit)
                {
                    Destroy(gameObject);
                }
            }
           
        }

        if (collision.gameObject.GetComponent<MonsterChargerr>())
        {
            if (IsPopped && !collision.gameObject.GetComponent<MonsterChargerr>().DeathCheck())
            {
                Instantiate(HitParticlePrefab, transform.position, Quaternion.identity);
                Instantiate(HitSound, transform.position, Quaternion.identity);
                if (DestroyItemOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (collision.gameObject.GetComponent<BossHealth>())
        {
            if (IsPopped && !collision.gameObject.GetComponent<BossHealth>().DeathCheck())
            {
                Instantiate(HitParticlePrefab, transform.position, Quaternion.identity);
                Instantiate(HitSound, transform.position, Quaternion.identity);
                if (DestroyItemOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (collision.gameObject.GetComponent<BubbleBehavior>())
        {
            if (IsPopped)
            {
                Instantiate(HitParticlePrefab, transform.position, Quaternion.identity);
                Instantiate(HitSound, transform.position, Quaternion.identity);
                if (DestroyItemOnHit)
                {
                    Destroy(gameObject);
                }
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

    public bool PoppedCheck()
    {
        return IsPopped;
    }
}
