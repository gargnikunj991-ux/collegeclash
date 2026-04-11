using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text timerText;
    private float score = 0f;
    public TMP_Text scoreText;
    public enum GameState
    {
        Start,
        Playing,
        End
    }
    public GameObject endPanel;

    public GameState currentState;

    public float matchTime = 60f;
    private float timer;

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
        timerText.text ="Timer:"+ Mathf.Ceil(timer).ToString();
        if (currentState == GameState.Playing)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                EndMatch();
            }
        }
    }

    public void StartMatch()
    {
        currentState = GameState.Playing;
        timer = matchTime;
        Time.timeScale = 1f;

        if (endPanel != null)
            endPanel.SetActive(false);

        currentState = GameState.Playing;
        timer = matchTime;
        Time.timeScale = 1f;

        score = 0;
        scoreText.text = "Score: 0";

        if (endPanel != null)
            endPanel.SetActive(false);
    }

    public void EndMatch()
    {
        currentState = GameState.End;
        Time.timeScale = 0f;

        Debug.Log("Match Ended");
        currentState = GameState.End;
        Time.timeScale = 0f;

        endPanel.SetActive(true);
    }

    public void RestartMatch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddScore(float amount)
    {
        score += amount;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

}