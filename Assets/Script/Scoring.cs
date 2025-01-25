using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    private int Score;
    private int Combo;
    private float ComboInterval;
    private float ComboDuration;
    private ObjectSpawner EntitySpawner;

    private void Start()
    {
        EntitySpawner = FindAnyObjectByType<ObjectSpawner>();
        ComboDuration = (60 / EntitySpawner.BPM) * 4;
    }

    private void Update()
    {
        if(ComboInterval > 0)
        {
            ComboInterval -= Time.deltaTime;
        }
        else
        {
            Combo = 1;
        }
    }
    public void OnScoring(int scoreearn)
    {
        Score += scoreearn * Combo;
        Combo++;
        ComboInterval = ComboDuration;

        //Debug.Log("Current Score :" + Score + " | Current Combo :" + Combo);
    }
}
