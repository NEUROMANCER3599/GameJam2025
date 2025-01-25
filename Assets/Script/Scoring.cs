using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring : MonoBehaviour
{
    public HighscoreRecord scoreRecord;
    private int Score;
    private int Combo;
    private int MaxCombo;
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

        if(Combo > MaxCombo)
        {
            MaxCombo = Combo;
        }

        //Debug.Log("Current Score :" + Score + " | Current Combo :" + Combo);
    }

    public void OnSummary()
    {
        if(Score > scoreRecord.Highscore)
        {
            scoreRecord.Highscore = Score;
        }
        if(MaxCombo > scoreRecord.MaxCombo)
        {
            scoreRecord.MaxCombo = MaxCombo;
        }


    }
    public float CheckComboInterval()
    {
        return ComboInterval;
    }

    public float CheckComboDuration()
    {
        return ComboDuration;
    }

    public int CheckScore()
    {
        return Score;
    }

    public int CheckCombo()
    {
        return Combo;
    }

    public int CheckMaxCombo()
    {
        return MaxCombo;
    }
}
