using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;
    public float JumpForce = 10;
    public string GroundCheckTag = "Ground";

    [Header("Components")]
    public SpriteRenderer PlayerSprite;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for ground
        /*
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit;
        ContactFilter2D filter;
        
        if(Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.down),))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        */

        Movement();
        Jumping();
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
    }

    void Jumping()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
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
