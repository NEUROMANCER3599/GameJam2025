using System;
using UnityEngine;

public class Monster2Movement : MonoBehaviour
{
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่
    public float chargeSpeed = 5f; // ความเร็วในการพุ่งเข้าหาผู้เล่น
    public float detectionRange = 5f; // ระยะที่ศัตรูสามารถตรวจจับผู้เล่น
    public GameObject player; // การอ้างอิงถึงตัวผู้เล่น
    private bool isCharging = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ในระยะการตรวจจับหรือไม่
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            isCharging = true;
        }

        if (isCharging)
        {
            ChargeAtPlayer();
        }
        else
        {
            MoveHorizontally();
        }
    }

    void MoveHorizontally()
    {
        // เคลื่อนที่ในแนวนอน
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void ChargeAtPlayer()
    {
        // พุ่งเข้าหาผู้เล่น
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * chargeSpeed * Time.deltaTime);
    }
}
