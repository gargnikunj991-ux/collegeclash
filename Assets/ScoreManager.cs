using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Adding Instance so CaptureZone can find this script easily
    public static ScoreManager Instance;

    [Header("Scores")]
    public int blueScore = 0;
    public int redScore = 0;

    [Header("UI")]
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    [Header("Match Settings")]
    public float matchTime = 300f; // Set to 300 for 5 minutes

    private float timer;
    private bool ended = false;

    void Awake()
    {
        // Setup the Singleton instance
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        timer = matchTime;

        if (winnerText != null)
            winnerText.gameObject.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (ended) return;

        timer -= Time.deltaTime;

        if (timer < 0)
            timer = 0;

        UpdateUI();

        if (timer <= 0)
        {
            EndMatch();
        }
    }

    // CaptureZone will call this function
    public void AddPoint(int team)
    {
        if (ended) return;

        if (team == 0)
            blueScore++;
        else if (team == 1)
            redScore++;

        UpdateUI();
    }

    void UpdateUI()
    {
        // Fixing the names here so they match your screen
        if (blueScoreText != null)
            blueScoreText.text = "Blue: " + blueScore;

        if (redScoreText != null)
            redScoreText.text = "Red: " + redScore;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.Ceil(timer);
    }

    void EndMatch()
    {
        ended = true;
        // Optional: comment out Time.timeScale if you want players to still move after it ends
        // Time.timeScale = 0f; 

        if (winnerText == null) return;

        winnerText.gameObject.SetActive(true);

        if (blueScore > redScore)
            winnerText.text = "BLUE TEAM WINS!";
        else if (redScore > blueScore)
            winnerText.text = "RED TEAM WINS!";
        else
            winnerText.text = "DRAW!";
    }
}