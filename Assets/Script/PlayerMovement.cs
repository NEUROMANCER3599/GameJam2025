using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;
    public float JumpForce = 10;
    public string GroundCheckTag = "Ground";

    [Header("Components")]
    public SpriteRenderer PlayerSprite;
    public Animator PlayerAnimator;
    public Rigidbody2D rb;
    public bool isGrounded;
    private PlayerHealth HealthModule;
    public GameObject JumpParticlePrefab;
    public Transform JumpParticleSpawnPos;
    public GameObject JumpSound;
    public GameObject LandingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HealthModule = GetComponent<PlayerHealth>();
    }

    void Update()
    {

        
        if (!HealthModule.PlayerDeathCheck())
        {
            PlayerAnimator.SetBool("IsJumped", !isGrounded);
            Movement();
            Jumping();
        }
        else
        {
            rb.freezeRotation = false;
            PlayerAnimator.SetBool("IsDead", true);
        }
        
    }

    void FixedUpdate()
    {

       
       
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move horizontally
        if (!isGrounded)
        {
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        }

        if (horizontalInput > 0)
        {
            PlayerSprite.flipX = true;
        }
        else
        {
            PlayerSprite.flipX = false;
        }

        PlayerAnimator.SetFloat("PlayerYVelocity",rb.linearVelocityY);
    }

    void Jumping()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                Instantiate(JumpParticlePrefab, JumpParticleSpawnPos.position, Quaternion.identity);
                Instantiate(JumpSound, JumpParticleSpawnPos.position, Quaternion.identity);
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                
            }
            
        }
        //Diving
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!isGrounded)
            {
                rb.gravityScale = 1 * speed;
            }
        }

        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.gravityScale = 1;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == GroundCheckTag)
        {
            isGrounded = true;
            Instantiate(LandingSound, JumpParticleSpawnPos.position, Quaternion.identity);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GroundCheckTag)
        {
            isGrounded = false;
            
        }
    }
    
    
}
