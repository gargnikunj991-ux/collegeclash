using UnityEngine;

public class Zone : MonoBehaviour
{
    public float captureTime = 3f;
    public float scoreRate = 1f;

    private float progress = 0f;

    private int playerCount = 0;
    private int enemyCount = 0;
    private Renderer zoneRenderer;
    private Material zoneMaterial;

    private enum ZoneOwner
    {
        Neutral,
        Player,
        Enemy
    }

    private ZoneOwner currentOwner = ZoneOwner.Neutral;

    // colors
    private Color neutralColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
    private Color playerColor = new Color(0f, 1f, 0f, 0.5f);
    private Color enemyColor = new Color(1f, 0f, 0f, 0.5f);

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();

        // IMPORTANT: unique material instance
        zoneMaterial = zoneRenderer.material;

        UpdateColor();
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;


        // CONTESTED
        if (playerCount > 0 && enemyCount > 0)
        {
            progress += Time.deltaTime * (playerCount - enemyCount);
            ShowContestedEffect();
            return;
        }

        // PLAYER CAPTURE
        if (playerCount > 0)
        {
            progress += Time.deltaTime * playerCount;
            Debug.Log("Player" + progress);

            if (progress >= captureTime)
            {
                currentOwner = ZoneOwner.Player;
                progress = captureTime;
                UpdateColor();
            }
        }

        // ENEMY CAPTURE
        else if (enemyCount > 0)
        {
            progress -= Time.deltaTime * enemyCount;
            Debug.Log("Enemy" + progress);
            if (progress <= 0)
            {
                currentOwner = ZoneOwner.Enemy;
                progress = 0;
                UpdateColor();
            }
        }

        // DECAY
        else
        {
            progress = Mathf.MoveTowards(progress, captureTime / 2f, Time.deltaTime);
        }

        UpdateVisualProgress();
        UpdateColor(); // keep color consistent when not contested
    }

    void UpdateVisualProgress()
    {
        float t = progress / captureTime;
        float scale = Mathf.Lerp(0.6f, 1.2f, t);

        transform.localScale = new Vector3(5 * scale, 0.3f, 5 * scale);
    }

    void UpdateColor()
    {
        if (currentOwner == ZoneOwner.Player)
            zoneMaterial.color = playerColor;

        else if (currentOwner == ZoneOwner.Enemy)
            zoneMaterial.color = enemyColor;

        else
            zoneMaterial.color = neutralColor;

        // optional glow (URP)
        zoneMaterial.EnableKeyword("_EMISSION");
        zoneMaterial.SetColor("_EmissionColor", zoneMaterial.color * 1.5f);
    }

    void ShowContestedEffect()
    {
        float pulse = Mathf.PingPong(Time.time * 5f, 1f);

        Color contested = Color.Lerp(
            new Color(1f, 1f, 0f, 0.6f), // yellow
            new Color(1f, 0f, 0f, 0.6f), // red
            pulse
        );

        zoneMaterial.color = contested;

        zoneMaterial.EnableKeyword("_EMISSION");
        zoneMaterial.SetColor("_EmissionColor", contested * 2f);
    }
    // handle score rate for both enemy and player 
    public float GetScoreRateForPlayer()
    {
        if (currentOwner == ZoneOwner.Player && enemyCount == 0)
            return scoreRate;

        return 0f;
    }

    public float GetScoreRateForEnemy()
    {
        if (currentOwner == ZoneOwner.Enemy && playerCount == 0)
            return scoreRate;

        return 0f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerCount++;

        if (other.CompareTag("Enemy"))
            enemyCount++;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerCount--;

        if (other.CompareTag("Enemy"))
            enemyCount--;
    }
}