using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    private int teamAPount = 0;
    private int teamBCount = 0;

    void Update()
    {
        if (teamAPount > 0 && teamBCount == 0)
        {
            AwardPoints("Team A");
        }
        else if (teamBCount > 0 && teamAPount == 0)
        {
            AwardPoints("Team B");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerTeam p = other.GetComponent<PlayerTeam>();
        if (p != null)
        {
            // Use the Enum instead of a string
            if (p.team == TeamSide.TeamA) teamAPount++;
            if (p.team == TeamSide.TeamB) teamBCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerTeam p = other.GetComponent<PlayerTeam>();
        if (p != null)
        {
            if (p.team == TeamSide.TeamA) teamAPount--;
            if (p.team == TeamSide.TeamB) teamBCount--;
        }
    }

    // Removed unused 'team' parameter to fix IDE0060
    void AwardPoints(string teamName)
    {
        Debug.Log(teamName + " is capturing!");
    }
}