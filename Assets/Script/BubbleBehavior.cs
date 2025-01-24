using UnityEditor.Rendering;
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    [Header("Customization Parameters")]
    public bool IsStartingBubble;
    public float minSize = 0.5f; 
    public float maxSize = 2f; 
    public float baseSpeed = 2f;
    

    [Header("System")]
    private Rigidbody2D rb;
    private float moveSpeed;
    private float randomSize;
    private float Lifespan;
    private float TouchedLifeSpan;
    private bool IsTouched = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

        randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1f);
        moveSpeed = baseSpeed / randomSize;
        Lifespan = randomSize * 15;
        TouchedLifeSpan = Lifespan * 0.25f;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(0, moveSpeed);
        

        Lifespan -= 1f * Time.deltaTime;

        if(Lifespan <= 0)
        {
            BubbleDestruction();
        }

        if (IsTouched)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                BubbleDestruction();
            }
        }
    }

   void BubbleDestruction()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (!IsTouched)
            {
                Lifespan = TouchedLifeSpan;
                IsTouched = true;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
           IsTouched = false;

        }
    }

}
