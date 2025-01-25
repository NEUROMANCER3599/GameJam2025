using UnityEngine;

public class StartingBaseBehavior : MonoBehaviour
{

    public Animator BubbleAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //BubbleAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            BubbleAnimator.SetBool("IsSteppedOn", true);
            

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
      
            BubbleAnimator.SetBool("IsSteppedOn", false);

    }
}
