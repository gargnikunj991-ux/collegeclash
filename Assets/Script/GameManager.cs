using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject endPanel;

    [Header("Game Settings")]
    public float matchTime = 60f;

    [Header("Zones")]
    public List<Zone> zones = new List<Zone>();

    private float timer;
    private float score = 0f;

    public enum GameState
    {
        Start,
        Playing,
        End
    }

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartMatch();
    }

    void Update()
    {
        if (currentState != GameState.Playing) return;

        // TIMER
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            EndMatch();
            return;
        }

        // SCORE SYSTEM (CLEAN + SAFE)
        float playerScoreRate = 0f;

        foreach (Zone z in zones)
        {
            if (z == null) continue;

            playerScoreRate += z.GetScoreRateForPlayer();
        }

        // HARD LIMIT (prevents bugs/explosions)
        playerScoreRate = Mathf.Clamp(playerScoreRate, 0f, 3f);

        AddScore(playerScoreRate * Time.deltaTime);
    }

    public void StartMatch()
    {
        currentState = GameState.Playing;
        timer = matchTime;
        Time.timeScale = 1f;

        score = 0f;
        UpdateScoreUI();

        if (endPanel != null)
            endPanel.SetActive(false);
    }

    public void AddScore(float amount)
    {
        // FINAL SAFETY
        amount = Mathf.Clamp(amount, 0f, 5f);

        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void EndMatch()
    {
        currentState = GameState.End;
        Time.timeScale = 0f;

        if (endPanel != null)
            endPanel.SetActive(true);

        ShowResult();
    }

    void ShowResult()
    {
        int finalScore = Mathf.FloorToInt(score);

        if (finalScore >= 50)
            Debug.Log("YOU WIN");
        else
            Debug.Log("YOU LOSE");
    }

    public void RestartMatch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}