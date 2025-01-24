using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Parameters")]
    private Transform playerTransform;
    public GameObject objectPrefab; // พรีแฟบของวัตถุที่ต้องการสุ่มเกิด
    public float spawnInterval = 1f; // ระยะเวลาระหว่างการเกิด
    public float minX = -5f; // ตำแหน่ง X ต่ำสุด
    public float maxX = 5f; // ตำแหน่ง X สูงสุด
    public float startY = -5f; // ตำแหน่ง Y ที่วัตถุเริ่มเกิด
    public float minSize = 0.5f; // ขนาดเล็กสุดของวัตถุ
    public float maxSize = 2f; // ขนาดใหญ่สุดของวัตถุ
    public float baseSpeed = 5f; // ความเร็วพื้นฐาน

    void Start()
    {
        // เรียกใช้ฟังก์ชัน SpawnObject ซ้ำ ๆ
        playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        InvokeRepeating("SpawnObject", 0f, spawnInterval);

    }

    void SpawnObject()
    {
        // สุ่มตำแหน่ง X
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y - startY, 0f);

        // สร้างวัตถุ
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

        // สุ่มขนาดของวัตถุ
        float randomSize = Random.Range(minSize, maxSize);
        newObject.transform.localScale = new Vector3(randomSize, randomSize, 1f);

        // คำนวณความเร็วตามขนาด (ยิ่งเล็กยิ่งเร็ว)
        float moveSpeed = baseSpeed / randomSize;

        // เพิ่ม Rigidbody2D และตั้งค่า velocity
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = newObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0; // ปิดแรงโน้มถ่วง
        rb.linearVelocity = new Vector2(0, moveSpeed); // กำหนดให้วัตถุเลื่อนขึ้นในแนว Y
        
        Destroy(newObject,10f);
    }
    
}
