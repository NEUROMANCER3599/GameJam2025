using UnityEngine;
using UnityEngine.UIElements;

public class MonsterChargerr : MonoBehaviour
{
    private PlayerMovement player;
    private Rigidbody2D rb;
    private float moveSpeed;
    private bool IsDead = false;
    private bool IsTouched = false;
    private bool IsCharging = false;
    private float detectionRange = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerMovement>(); // หา GameObject ที่มี Tag "Player"
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(15, 20);
    }

    private void Update()
    {
       

        

        if (!IsDead)
        {
            ScanningPlayer();
            if (IsTouched && !IsCharging)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Death();
                }
            }

            if (IsCharging)
            {
                ChargePlayer();
            }
            else
            {
                Movement();
            }
        }

       
    }

    void ScanningPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            IsCharging = true;
        }
    }

    void ChargePlayer()
    {
        
        //Vector2 direction = (player.transform.position - transform.position).normalized;
        //rb.gravityScale = 0;
        if(transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(moveSpeed * 1.5f, 0);
        }
        else if(transform.position.x > player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(-moveSpeed * 1.5f, 0);
        }
        
    }

    void Movement()
    {
        if (!IsDead && !IsCharging)
        {
            rb.gravityScale = 0; 
            rb.linearVelocity = new Vector2(0, moveSpeed); 
        }

    }

    void Death()
    {
        IsDead = true;
        rb.gravityScale = 1;
        rb.freezeRotation = false;


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
                //BubbleAnimator.SetBool("IsSteppedOn", true);
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

        if (collision.gameObject.GetComponent<MonsterChargerr>())
        {
            MonsterChargerr monsterentity = collision.gameObject.GetComponent<MonsterChargerr>();
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
            //BubbleAnimator.SetBool("IsSteppedOn", false);
            IsTouched = false;
        }
    }

    public bool DeathCheck()
    {
        return IsDead;
    }
}
