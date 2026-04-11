using UnityEngine;

public class Zone : MonoBehaviour
{
    public float captureTime = 3f;   // time to capture
    public float scoreRate = 5f;     // points per second after capture

    private float captureProgress = 0f;

    private bool playerInside = false;
    private bool enemyInside = false;

    private Renderer zoneRenderer;

    private enum ZoneState
    {
        Neutral,
        Captured
    }

    private ZoneState currentState = ZoneState.Neutral;

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        // STOP everything if enemy is inside (contest)
        if (enemyInside)
        {
            return;
        }

        // CAPTURE LOGIC
        if (playerInside && currentState == ZoneState.Neutral)
        {
            captureProgress += Time.deltaTime;

            Debug.Log("Capture: " + captureProgress);

            if (captureProgress >= captureTime)
            {
                currentState = ZoneState.Captured;
                UpdateColor();
                Debug.Log("Zone Captured!");
            }
        }

        // SCORE AFTER CAPTURE
        if (currentState == ZoneState.Captured)
        {
            GameManager.Instance.AddScore(scoreRate * Time.deltaTime);
        }

        // RESET capture if player leaves early
        if (!playerInside && currentState == ZoneState.Neutral)
        {
            captureProgress = 0f;
        }
    }

    void UpdateColor()
    {
        if (zoneRenderer == null) return;

        if (currentState == ZoneState.Neutral)
        {
            zoneRenderer.material.color = Color.gray;
        }
        else if (currentState == ZoneState.Captured)
        {
            zoneRenderer.material.color = Color.green;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }

        if (other.CompareTag("Enemy"))
        {
            enemyInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }

        if (other.CompareTag("Enemy"))
        {
            enemyInside = false;
        }
    }
}