using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text playerScoreText;
    public TMP_Text enemyScoreText;
    public GameObject endPanel;
    public TMP_Text resultText;

    [Header("Game Settings")]
    public float matchTime = 60f;

    [Header("Zones")]
    public List<Zone> zones = new List<Zone>();

    private float timer;
    private float playerScore = 0f;
    private float enemyScore = 0f;

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

        float playerRate = 0f;
        float enemyRate = 0f;

        foreach (Zone z in zones)
        {
            if (z == null) continue;

            playerRate += z.GetScoreRateForPlayer();
            enemyRate += z.GetScoreRateForEnemy();
        }

        // clamp for safety
        playerRate = Mathf.Clamp(playerRate, 0f, 3f);
        enemyRate = Mathf.Clamp(enemyRate, 0f, 3f);

        // apply
        playerScore += playerRate * Time.deltaTime;
        enemyScore += enemyRate * Time.deltaTime;

        // update UI
        UpdateScoreUI();
    }

    public void StartMatch()
    {
        currentState = GameState.Playing;
        timer = matchTime;
        Time.timeScale = 1f;

        playerScore = 0f;
        enemyScore = 0f;
        UpdateScoreUI();

        if (endPanel != null)
            endPanel.SetActive(false);
    }

    

    void UpdateScoreUI()
    {
        playerScoreText.text = "Player: " + Mathf.FloorToInt(playerScore);
        enemyScoreText.text = "Enemy: " + Mathf.FloorToInt(enemyScore);
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
        int p = Mathf.FloorToInt(playerScore);
        int e = Mathf.FloorToInt(enemyScore);

        if (p > e)
        {
            resultText.text = "YOU WIN";
        }
        else if (e > p)
        {
            resultText.text = "YOU LOSE";
        }
        else
        {
            resultText.text = "DRAW";
        }
    }

    public void RestartMatch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}