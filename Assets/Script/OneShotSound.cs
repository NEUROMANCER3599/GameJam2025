using UnityEngine;

public class OneShotSound : MonoBehaviour
{
    private AudioSource audioSource;
    private float DespawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DespawnTime = audioSource.clip.length;
        Destroy(gameObject,DespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
