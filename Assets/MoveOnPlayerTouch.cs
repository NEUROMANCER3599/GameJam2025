using UnityEngine;

public class MoveOnPlayerTouch : MonoBehaviour
{  public float moveSpeed = 2f; // ความเร็วในการขยับขึ้น
    private bool isActivated = false; // ใช้เช็คว่าเริ่มขยับหรือยัง

    private Rigidbody2D rb;

    void Start()
    {
        // เพิ่ม Rigidbody2D ให้กับวัตถุหากยังไม่มี
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0; // ปิดแรงโน้มถ่วง
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // หยุดการเคลื่อนไหวเริ่มต้น
    }

    void Update()
    {
        // ถ้า isActivated เป็น true ให้ขยับขึ้นในแนว Y
        if (isActivated)
        {
            rb.velocity = new Vector2(0, moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // เช็คว่าผู้เล่นชนด้านบนของวัตถุ
        if (collision.collider.CompareTag("Player"))
        {
            // ตรวจสอบตำแหน่งว่าผู้เล่นอยู่ด้านบน
            if (collision.contacts[0].normal.y < -0.5f)
            {
                isActivated = true; // เปิดให้วัตถุเริ่มขยับขึ้น
                rb.constraints = RigidbodyConstraints2D.None; // ปลดล็อกการเคลื่อนไหว
            }
        }
    }
}
