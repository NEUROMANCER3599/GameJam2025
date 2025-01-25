using UnityEngine;

public class AutoDespawner : MonoBehaviour
{
    [SerializeField] private float DespawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,DespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
