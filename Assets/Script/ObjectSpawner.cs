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
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

      

       
 
    }
    
}
