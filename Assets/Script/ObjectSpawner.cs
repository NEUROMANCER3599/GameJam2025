using UnityEngine;
using System.Collections.Generic;
public class ObjectSpawner : MonoBehaviour
{
    [Header("Parameters")]
  
    private Transform playerTransform;
    public GameObject BubblePrefab; // พรีแฟบของวัตถุที่ต้องการสุ่มเกิด
    public List<GameObject> MonsterPrefabs;
    public List<GameObject> ItemPrefabs;
    
    public float minX = -5f; // ตำแหน่ง X ต่ำสุด
    public float maxX = 5f; // ตำแหน่ง X สูงสุด
    public float startY = -5f; // ตำแหน่ง Y ที่วัตถุเริ่มเกิด

    [Header("System")]
    public float BPM;
    private  float BubblespawnDuration;
    private  float EnemySpawnDuration;
    private float BubbleSpawnInterval;
    private float EnemySpawnInterval;
    private float ItemSpawnDuration;
    private float ItemSpawnInterval;
   


    void Start()
    {
      
        // เรียกใช้ฟังก์ชัน SpawnObject ซ้ำ ๆ
        OnBegin();
        playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;
        //InvokeRepeating("SpawnObject", 0f, spawnInterval);

    }
    public void OnBegin()
    {
        BubblespawnDuration = 60 / BPM;
        EnemySpawnDuration = BubblespawnDuration * 4f;
        ItemSpawnDuration = BubblespawnDuration * 16f;
        BubbleSpawnInterval = BubblespawnDuration;
        EnemySpawnInterval = EnemySpawnDuration;
        ItemSpawnInterval = ItemSpawnDuration;
        
    }

    private void Update()
    {
        if (BubbleSpawnInterval > 0)
        {
            BubbleSpawnInterval -= 1f * Time.deltaTime;
        }
        else
        {
            SpawnBubbles();
            BubbleSpawnInterval = BubblespawnDuration;
        }

        if (EnemySpawnInterval > 0)
        {
            EnemySpawnInterval -= 1f * Time.deltaTime;
        }
        else
        {
            SpawnMonster();
            
            EnemySpawnInterval = Random.Range(EnemySpawnDuration,EnemySpawnDuration * 1.5f);
        }

        if(ItemSpawnInterval > 0)
        {
            ItemSpawnInterval -= 1f * Time.deltaTime;
        }
        else
        {
            SpawnItems();

            ItemSpawnInterval = Random.Range(ItemSpawnDuration, ItemSpawnDuration * 2f);
        }
    }

    public void SpawnBubbles()
    {
        // สุ่มตำแหน่ง X
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y - startY, 0f);

        // สร้างวัตถุ
        Instantiate(BubblePrefab, spawnPosition, Quaternion.identity);
 
    }

    public void SpawnMonster()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y - startY, 0f);


        // สร้างวัตถุ
        Instantiate(MonsterPrefabs[Random.Range(0,MonsterPrefabs.Count)], spawnPosition, Quaternion.identity);

    }

    public void SpawnItems()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y - startY, 0f);


        // สร้างวัตถุ
        Instantiate(ItemPrefabs[Random.Range(0, ItemPrefabs.Count)], spawnPosition, Quaternion.identity);

    }


}
