using UnityEngine;

public class Zone : MonoBehaviour
{
    public float captureTime = 3f;
    public float scoreRate = 1f;

    private float progress = 0f;

    private bool playerInside = false;
    private bool enemyInside = false;

    private Renderer zoneRenderer;

    private enum ZoneOwner
    {
        Neutral,
        Player,
        Enemy
    }

    private ZoneOwner currentOwner = ZoneOwner.Neutral;

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        // CONTEST → STOP EVERYTHING
        if (playerInside && enemyInside)
        {
            progress = 0f;
            return;
        }

        // PLAYER CAPTURING
        if (playerInside && !enemyInside)
        {
            if (currentOwner != ZoneOwner.Player)
            {
                progress += Time.deltaTime;

                if (progress >= captureTime)
                {
                    currentOwner = ZoneOwner.Player;
                    progress = 0f;
                    UpdateColor();
                    Debug.Log("Player captured zone");
                }
            }
        }
        

        // ENEMY CAPTURING
        if (enemyInside && !playerInside)
        {
            if (currentOwner != ZoneOwner.Enemy)
            {
                progress += Time.deltaTime;

                if (progress >= captureTime)
                {
                    currentOwner = ZoneOwner.Enemy;
                    progress = 0f;
                    UpdateColor();
                    Debug.Log("Enemy captured zone");
                }
            }
        }

        // RESET if nobody inside
        if (!playerInside && !enemyInside)
        {
            return;
        }
    }

    public float GetScoreRateForPlayer()
    {
        if (currentOwner == ZoneOwner.Player)
            return scoreRate;

        return 0f;
    }

    public float GetScoreRateForEnemy()
    {
        if (currentOwner == ZoneOwner.Enemy)
            return scoreRate;

        return 0f;
    }

    void UpdateColor()
    {
        if (zoneRenderer == null) return;

        if (currentOwner == ZoneOwner.Neutral)
        {
            zoneRenderer.material.color = Color.gray;
        }
        else if (currentOwner == ZoneOwner.Player)
        {
            zoneRenderer.material.color = Color.green;
        }
        else if (currentOwner == ZoneOwner.Enemy)
        {
            zoneRenderer.material.color = Color.red;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;

        if (other.CompareTag("Enemy"))
            enemyInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;

        if (other.CompareTag("Enemy"))
            enemyInside = false;
    }
}