using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Scores")]
    public int blueScore = 0;
    public int redScore = 0;

    [Header("UI")]
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    [Header("Match Settings")]
    public float matchTime = 120f;

    private float timer;
    private bool ended = false;

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

    public void AddPoint(int team)
    {
        if (team == 0)
            blueScore++;

        else if (team == 1)
            redScore++;

        UpdateUI();
    }

    void UpdateUI()
    {
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
        Time.timeScale = 0f;

        if (winnerText == null) return;

        winnerText.gameObject.SetActive(true);

        if (blueScore > redScore)
            winnerText.text = "Blue Wins!";
        else if (redScore > blueScore)
            winnerText.text = "Red Wins!";
        else
            winnerText.text = "Draw!";
    }
}