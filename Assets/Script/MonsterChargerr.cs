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
            if (IsTouched)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Death();
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
        IsDead = true;
        rb.gravityScale = 2;
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

        /*
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (!IsCharging)
            {
                playerHealth.TakeDamage(1);
            }
           
        }
        */

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
