using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    // Team inside counts
    int bluePlayers = 0;
    int redPlayers = 0;

// Zone ownership
int currentTeam = -1;   // -1 = neutral, 0 = blue, 1 = red
    int teamInside = -1;

    // Capture timing
    float captureTimer = 0f;
    public float captureTime = 3f;

    // Score timing
    float scoreTimer = 0f;

    // Materials
    public Material neutralMaterial;
    public Material blueMaterial;
    public Material redMaterial;
    public Material contestedMaterial;

    Renderer zoneRenderer;
    ScoreManager scoreManager;

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        zoneRenderer.material = neutralMaterial;

        scoreManager = FindFirstObjectByType<ScoreManager>(); // FIXED
    }

    void Update()
    {
        // Determine who controls the zone
        if (bluePlayers > 0 && redPlayers == 0)
        {
            teamInside = 0;
        }
        else if (redPlayers > 0 && bluePlayers == 0)
        {
            teamInside = 1;
        }
        else
        {
            teamInside = -1;
        }

        // Show contested color
        if (bluePlayers > 0 && redPlayers > 0)
        {
            zoneRenderer.material = contestedMaterial;
        }

        // Capture logic
        if (teamInside != -1)
        {
            captureTimer += Time.deltaTime;

            if (captureTimer >= captureTime)
            {
                currentTeam = teamInside;
                captureTimer = 0f;

                if (currentTeam == 0)
                    zoneRenderer.material = blueMaterial;

                if (currentTeam == 1)
                    zoneRenderer.material = redMaterial;
            }
        }

        // Score while owned
        if (currentTeam != -1)
        {
            scoreTimer += Time.deltaTime;

            if (scoreTimer >= 1f)
            {
                scoreTimer = 0f;

                if (scoreManager != null)
                {
                    scoreManager.AddPoint(currentTeam);
                }
            }
        }
    }void OnTriggerEnter(Collider other)
    {
        PlayerTeam team = other.GetComponent<PlayerTeam>();

        if (team != null)
        {
            if (team.teamID == 0)
                bluePlayers++;

            if (team.teamID == 1)
                redPlayers++;
        }
    }


 

void OnTriggerExit(Collider other)
    {
        PlayerTeam team = other.GetComponent<PlayerTeam>();

        if (team != null)
        {
            if (team.teamID == 0)
                bluePlayers--;

            if (team.teamID == 1)
                redPlayers--;
        }
    }

}
