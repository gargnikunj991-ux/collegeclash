using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    int score = 0;
    float time = 120f;

    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(time).ToString();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}