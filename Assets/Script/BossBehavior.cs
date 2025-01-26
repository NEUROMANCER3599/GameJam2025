using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("System")]
    private Vector2 velocity = Vector2.zero; // ใช้เก็บความเร็วในการเคลื่อนที่แบบสมูท
    private float randomXOffset; // ค่า offset ในแกน X สำหรับการสุ่ม
    private float timeSinceLastMove = 0f; // ตัวจับเวลาสำหรับการเปลี่ยนตำแหน่งสุ่ม
    private BossHealth _bossHealth;
    [Header("Movement")]
    public Transform player; // ตำแหน่งของผู้เล่น
    public float followSpeed = 2f; // ความเร็วของบอส
    public float smoothTime = 0.3f; // ระยะเวลาที่ใช้ในการเคลื่อนที่ให้สมูท
    public float randomMoveInterval = 2f; // ช่วงเวลาที่เปลี่ยนตำแหน่งสุ่ม
    public float distanceBetweenPlayer = 3f;
    public float moveRange = 2f; // ระยะที่บอสสามารถขยับซ้าย-ขวา
    public float minX = -4f; // ขอบเขตซ้ายสุด
    public float maxX = 4f;  // ขอบเขตขวาสุด
   

    [Header("Skill Settings")]
    public GameObject[] skills; // รายการของสกิลที่บอสสามารถ Spawn ได้
    public float skillSpawnInterval = 5f; // ระยะเวลาระหว่างการ Spawn สกิล
    private float skillTimer = 0f; // ตัวจับเวลาสำหรับการ Spawn สกิล
    public float cooldownTime = 5f; // เวลาที่ต้องรอก่อนใช้สกิลใหม่
    private float timeSinceLastSkill = 0f; // ตัวจับเวลาสำหรับการใช้งานสกิลล่าสุด

    [Header("Animator")]
    public Animator BossAnimator;
    private void Start()
    {
        _bossHealth = GetComponent<BossHealth>();
        // กำหนดค่าเริ่มต้นแบบสุ่มในแกน X
        randomXOffset = Random.Range(-moveRange, moveRange);

        // ค้นหาผู้เล่นด้วย Tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please assign a GameObject with the 'Player' tag.");
        }
    }

    private void Update()
    {
        // ตรวจสอบว่ามีผู้เล่นหรือไม่
        if (player == null) return;

        // เพิ่มเวลาที่ผ่านไป
        timeSinceLastMove += Time.deltaTime;
        skillTimer += Time.deltaTime;
        timeSinceLastSkill += Time.deltaTime;

        if (_bossHealth.DeathCheck() != false)
        {
            randomMoveInterval = 999;
            cooldownTime = 999;
        }
      
        // หากถึงเวลาที่ต้องเปลี่ยนตำแหน่งแกน X
        if (timeSinceLastMove >= randomMoveInterval)
        {
            randomXOffset = Random.Range(-moveRange, moveRange); // สุ่มตำแหน่งใหม่ในแกน X
            timeSinceLastMove = 0f; // รีเซ็ตตัวจับเวลา
        }

        // ตำแหน่งปัจจุบันของบอส
        Vector2 currentPosition = transform.position;
        // คำนวณตำแหน่งเป้าหมายในแกน X
        float targetX = Mathf.Clamp(/*player.position.x + */randomXOffset, minX, maxX); // จำกัดค่าที่ -4 ถึง 4
        
        // กำหนดตำแหน่งเป้าหมาย (แกน X ที่จำกัด, แกน Y ตามผู้เล่น)
        Vector2 targetPosition = new Vector2(targetX, player.position.y - distanceBetweenPlayer);

        // เคลื่อนที่ไปยังตำแหน่งเป้าหมายอย่างสมูท
        transform.position = Vector2.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime, followSpeed);
        
        if (skillTimer >= skillSpawnInterval && timeSinceLastSkill >= cooldownTime)
        {
            SpawnRandomSkill();
            BossAnimator.SetTrigger("OnAttack");
            skillTimer = 0f; // รีเซ็ตตัวจับเวลา
            timeSinceLastSkill = 0f; // รีเซ็ตตัวจับเวลาสำหรับสกิลล่าสุด
        }
    }
    
    // ฟังก์ชันสำหรับการ Spawn สกิลแบบสุ่ม
    private void SpawnRandomSkill()
    {
        if (skills.Length == 0) return;

        // เลือกสกิลแบบสุ่ม
        int randomIndex = Random.Range(0, skills.Length);
        GameObject selectedSkill = skills[randomIndex];

        // สร้างสกิลในตำแหน่งของบอส และตั้งบอสเป็น parent
        GameObject spawnedSkill = Instantiate(selectedSkill, transform);
        spawnedSkill.transform.localPosition = new Vector3(0f, 0f, 0f); // ปรับตำแหน่งใน Local Space
    }
    
    
}
