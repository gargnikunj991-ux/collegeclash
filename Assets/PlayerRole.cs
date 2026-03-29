using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    public enum RoleType { Tank, Scout, Support }
    public RoleType currentRole;

    private PlayerMovement movement;
    private AbilityHandler handler;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        handler = GetComponent<AbilityHandler>();

        ApplyRoleStats();
    }

    void ApplyRoleStats()
    {
        switch (currentRole)
        {
            case RoleType.Tank:
                movement.speed = 4f; // Slower but harder to push
                // Reference your PowerPush ability here
                break;
            case RoleType.Scout:
                movement.speed = 8f; // Fast
                // Reference a Dash ability here
                break;
            case RoleType.Support:
                movement.speed = 6f;
                // Reference the SlowField ability here
                break;
        }
    }
}