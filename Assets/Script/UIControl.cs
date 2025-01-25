using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject SummaryPanel;

    [Header("Components | GameUI")]
    [SerializeField] private Image Heart3;
    [SerializeField] private Image Heart2;
    [SerializeField] private Image Heart1;
    [SerializeField] private TextMeshProUGUI ScoreTxt;
    [SerializeField] private TextMeshProUGUI ComboTxt;
    [SerializeField] private Slider ComboTimer;
    [SerializeField] private Image ComboTImerFill;
    [SerializeField] private Sprite ValidHeart;
    [SerializeField] private Sprite InvalidHeart;

    [Header("Components | GameOver")]
    [SerializeField] private HighscoreRecord scoreRecord;
    [SerializeField] private TextMeshProUGUI TotalScoreTxt;
    [SerializeField] private TextMeshProUGUI HighScoreTxt;
    [SerializeField] private TextMeshProUGUI TotalComboTxt;
    [SerializeField] private TextMeshProUGUI BestComboTxt;

    [Header("Reference")]
    private PlayerHealth playerHealth;
    private Scoring ScoringModule;
    
 
    //[Header("Game UI Components")]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        ScoringModule = FindAnyObjectByType<Scoring>();
        
        InGameUI.SetActive(true);
        SummaryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InGameUI.activeSelf)
        {

            ScoreTxt.text = ScoringModule.CheckScore().ToString();

            ReactiveComboBar();
            HealthIndicator();
            ReactiveComboNum();
        }

       
    }

    void ReactiveComboNum()
    {
        
        ComboTxt.text = "x" + ScoringModule.CheckCombo().ToString();

        if(ScoringModule.CheckCombo() < 10)
        {
            ComboTxt.color = Color.white;
        }
        else if(ScoringModule.CheckCombo() > 10 && ScoringModule.CheckCombo() < 20)
        {
            ComboTxt.color = Color.yellow;
        }
        else if(ScoringModule.CheckCombo() > 20 && ScoringModule.CheckCombo() < 30)
        {
            ComboTxt.color = Color.cyan;
        }
        else if(ScoringModule.CheckCombo() > 30 && ScoringModule.CheckCombo() < 40)
        {
            ComboTxt.color = Color.green;
        }
        else if (ScoringModule.CheckCombo() > 40)
        {
            ComboTxt.color = Color.red;
        }

    }

    void ReactiveComboBar()
    {
        ComboTimer.maxValue = ScoringModule.CheckComboDuration();
        ComboTimer.value = ScoringModule.CheckComboInterval();

        if(ComboTimer.value > ComboTimer.maxValue * 0.5f)
        {
            ComboTimer.gameObject.SetActive(true);
            ComboTImerFill.color = Color.white;
        }
        else if (ComboTimer.value > ComboTimer.maxValue * 0.25f && ComboTimer.value < ComboTimer.maxValue * 0.5f)
        {
            ComboTImerFill.color = Color.yellow;
        }
        else if(ComboTimer.value < ComboTimer.maxValue * 0.25f)
        {
            ComboTImerFill.color = Color.red;
        }
        else if(ComboTimer.value <= 0)
        {
            ComboTimer.gameObject.SetActive(false);
        }

    }
    void HealthIndicator()
    {
        switch (playerHealth.ShowHealth())
        {
            case 3: Heart1.sprite = ValidHeart; Heart2.sprite = ValidHeart; Heart3.sprite = ValidHeart; break;
            case 2: Heart1.sprite = ValidHeart; Heart2.sprite = ValidHeart; Heart3.sprite = InvalidHeart; break;
            case 1: Heart1.sprite = ValidHeart; Heart2.sprite = InvalidHeart; Heart3.sprite = InvalidHeart; break;
            case 0: Heart1.sprite = InvalidHeart; Heart2.sprite = InvalidHeart; Heart3.sprite = InvalidHeart; break;
        }
    }

    public void OnGameOver()
    {
        ScoringModule.OnSummary();
        InGameUI.SetActive(false);
        SummaryPanel.SetActive(true);
        TotalScoreTxt.text = "Total Score: " + ScoringModule.CheckScore().ToString();
        TotalComboTxt.text = "Max Combo: " + ScoringModule.CheckMaxCombo().ToString();
        HighScoreTxt.text = "Highscore: " + scoreRecord.Highscore.ToString();
        BestComboTxt.text = "Best Combo: " + scoreRecord.MaxCombo.ToString();
        
    }
}
