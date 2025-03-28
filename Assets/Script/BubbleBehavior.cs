
using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{
    [Header("Customization Parameters")]
    public bool IsStartingBubble;
    public float minSize = 0.5f; 
    public float maxSize = 2f; 
    public float baseSpeed = 2f;
    public float SpeedCap;


    [Header("System")]
    public Animator BubbleAnimator;
    private CircleCollider2D circleCollider;
    private ObjectSpawner _objectSpawner;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private float moveSpeed;
    private float randomSize;
    private float Lifespan;
    private float TouchedLifeSpan;
    private bool IsTouched = false;
    private bool IsPopped = false;
    private Scoring scoring;
    public int BaseScore = 100;
    public int LifeBonus = 0;
    public GameObject PopSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        _objectSpawner = FindAnyObjectByType<ObjectSpawner>();
        scoring = FindAnyObjectByType<Scoring>();
        rb = GetComponent<Rigidbody2D>();
       playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();

        if (!IsStartingBubble)
        {
            randomSize = Random.Range(minSize, maxSize);
            transform.localScale = new Vector3(randomSize, randomSize, 1f);
            moveSpeed = baseSpeed / randomSize;
            if (moveSpeed > SpeedCap)
            {
                moveSpeed = SpeedCap;
            }
            Lifespan = randomSize * 15;
            TouchedLifeSpan = Lifespan * 0.1f;
        }
       
    }

    private void Update()
    {
       

        if (!IsStartingBubble)
        {
            rb.linearVelocity = new Vector2(0, moveSpeed);
            Lifespan -= 1f * Time.deltaTime;
        }
       
    
        if(Lifespan <= 0  && !IsStartingBubble && !IsPopped)
        {
            BubbleDestruction();
        }

        if (IsTouched)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                scoring.OnScoring(BaseScore);
                playerHealth.Heal(LifeBonus);
                BubbleDestruction();
            }
        }
    }

   void BubbleDestruction()
    {
        IsPopped = true;
        Instantiate(PopSound, transform.position, Quaternion.identity);
        circleCollider.enabled = false;
        BubbleAnimator.SetTrigger("OnDeath");
        Destroy(gameObject,1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (!IsTouched)
            {
                BubbleAnimator.SetBool("IsSteppedOn", true);
                Lifespan = TouchedLifeSpan;
                IsTouched = true;
            }
            
        }

        if (collision.gameObject.GetComponent<FallingItem>())
        {
            FallingItem item = collision.gameObject.GetComponent<FallingItem>();
            if (item.PoppedCheck() && LifeBonus == 0)
            {
                scoring.OnScoring(BaseScore);
                BubbleDestruction();
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cleaner")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Explosion")
        {
            scoring.OnScoring(BaseScore);
            BubbleDestruction();
        }
    }
}
