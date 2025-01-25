using UnityEngine;

public class CollisionDmg : MonoBehaviour
{
    public int Damage;
    public GameObject HitParticlePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHeath = collision.GetComponent<PlayerHealth>();
            Instantiate(HitParticlePrefab, transform.position, Quaternion.identity);
            playerHeath.TakeDamage(Damage);
        }
    }
    

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            PlayerHealth playerHeath = collision.gameObject.GetComponent<PlayerHealth>();
            playerHeath.TakeDamage(Damage);
        }
    }
    */
}
