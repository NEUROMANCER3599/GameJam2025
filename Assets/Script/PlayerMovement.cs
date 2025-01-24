using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;
    public float MinJumpForce = 10;
    public float MaxJumpForce = 15;
    public string GroundCheckTag = "Ground";

    [Header("Components")]
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for ground
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit;
        if(Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.down),0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {

        Movement();
        Jumping();
       
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move horizontally
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    void Jumping()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * MinJumpForce, ForceMode2D.Impulse);
        }
    }

    /*
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
    */
}
