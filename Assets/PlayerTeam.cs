using UnityEngine;

public enum TeamSide { None, TeamA, TeamB }
public enum UnitType { Scout, Tank, Controller }

public class PlayerTeam : MonoBehaviour
{
    public TeamSide team = TeamSide.None;
    public UnitType unitType;

    public float GetCaptureRate()
    {
        switch (unitType)
        {
            case UnitType.Scout: return 1f / 5f;       // fast (5 sec)
            case UnitType.Tank: return 1f / 10f;       // medium
            case UnitType.Controller: return 1f / 20f; // slow
        }
        return 0f;
    }
}