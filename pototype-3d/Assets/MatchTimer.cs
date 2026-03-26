using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    public float matchTime = 120f;

    void Update()
    {
        matchTime -= Time.deltaTime;

        if (matchTime <= 0)
        {
            Debug.Log("Match Ended");
            Time.timeScale = 0;
        }
    }
}