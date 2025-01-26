using UnityEngine;

public class PlayerJumpThrough : MonoBehaviour
{
  
   // public float jumpForce = 10f; // พลังการกระโดด
    public Rigidbody2D rb; // Rigidbody2D ของตัวละคร
    private bool canJumpThrough = false; // เช็คว่ากระโดดจากด้านล่างหรือไม่

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // กำหนด Rigidbody2D
    }

    void Update()
    {
        // ตรวจสอบการกระโดดขึ้น (จากด้านล่าง)
        if (Input.GetKeyDown(KeyCode.Space) && rb.linearVelocity.y < 0) // กระโดดจากด้านล่าง
        {
            canJumpThrough = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบการชนกับวงกลม
        if (collision.collider.CompareTag("Ground"))
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 platformPosition = collision.collider.transform.position;

            // เช็คว่าตัวละครเหยียบจากด้านบน
            if (contactPoint.y > platformPosition.y)
            {
                canJumpThrough = false; // ไม่ให้ทะลุเมื่อเหยียบจากด้านบน
            }
            else if (canJumpThrough)
            {
                // ถ้ากระโดดจากด้านล่างและสามารถทะลุได้
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true); // ทะลุผ่านวงกลม
              //  rb.velocity = new Vector2(rb.velocity.x, jumpForce); // กระโดดขึ้นไปจากด้านล่าง
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // รีเซ็ตการ IgnoreCollision เมื่อออกจากการชน
        if (collision.collider.CompareTag("Ground"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, false);
        }
    }
}
