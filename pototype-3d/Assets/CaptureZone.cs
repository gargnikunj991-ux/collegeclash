using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    bool playerInside = false;
    bool captured = false;

    float captureTimer = 0f;
    public float captureTime = 3f;

    float scoreTimer = 0f;
   
   

    public Material neutralMaterial;
    public Material capturingMaterial;
    public Material capturedMaterial;

    Renderer zoneRenderer;

    ScoreManager scoreManager;

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        zoneRenderer.material = neutralMaterial;

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (playerInside && !captured)
        {
            captureTimer += Time.deltaTime;

            zoneRenderer.material = capturingMaterial;

            if (captureTimer >= captureTime)
            {
                captured = true;
                zoneRenderer.material = capturedMaterial;
                Debug.Log("Zone Captured");
            }
        }

        if (captured)
        {
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= 1f)
            {
                scoreTimer = 0f;

                if (scoreManager != null)
                {
                    scoreManager.AddPoint();
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            captureTimer = 0f;

            if (!captured)
            {
                zoneRenderer.material = neutralMaterial;
            }
        }
    }
}