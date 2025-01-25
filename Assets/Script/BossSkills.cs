using System;
using UnityEngine;

public class BossSkills : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth.TakeDamage(1);
        }
    }
    
}
