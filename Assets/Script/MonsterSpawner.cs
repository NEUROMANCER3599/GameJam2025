using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject monsterPrefab; // พรีแฟบของวัตถุที่ต้องการสุ่มเกิด
    public float spawnInterval = 1f; // ระยะเวลาระหว่างการเกิด
    public float minX = -5f; // ตำแหน่ง X ต่ำสุด
    public float maxX = 5f; // ตำแหน่ง X สูงสุด
    public float startY = -5f; // ตำแหน่ง Y ที่วัตถุเริ่มเกิด
    public float moveSpeed = 2f; // ความเร็วที่วัตถุเคลื่อนที่ขึ้น

    void Start()
    {
        playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        // เรียกใช้ฟังก์ชัน SpawnObject ซ้ำ ๆ
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        // สุ่มตำแหน่ง X
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX,playerTransform.position.y - startY, 0f);

        // สร้างวัตถุ
        GameObject newObject = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // เพิ่ม Rigidbody2D และตั้งค่า velocity
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = newObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0; // ปิดแรงโน้มถ่วง
        rb.velocity = new Vector2(0, moveSpeed); // กำหนดให้วัตถุเลื่อนขึ้นในแนว Y
    }
}
