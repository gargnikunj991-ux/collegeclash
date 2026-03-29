using UnityEngine;

// This defines the only possible options for teams
public enum TeamSide { None, TeamA, TeamB }

public class PlayerTeam : MonoBehaviour
{
    // This creates a dropdown menu in the Unity Inspector
    public TeamSide team = TeamSide.None;
}