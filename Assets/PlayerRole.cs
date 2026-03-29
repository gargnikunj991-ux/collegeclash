using UnityEngine;

public class PlayerRole : MonoBehaviour
{
    public enum RoleType { Tank, Scout, Support }
    public RoleType currentRole;

    private AbilityHandler handler;

    void Start()
    {
        handler = GetComponent<AbilityHandler>();
        SetupRole();
    }

    void SetupRole()
    {
        // 1. Disable all abilities first to start clean
        if (GetComponent<PowerPush>()) GetComponent<PowerPush>().enabled = false;
        if (GetComponent<DashAbility>()) GetComponent<DashAbility>().enabled = false;
        if (GetComponent<SlowField>()) GetComponent<SlowField>().enabled = false;

        // 2. Enable only the one that matches the role
        switch (currentRole)
        {
            case RoleType.Tank:
                Ability push = GetComponent<PowerPush>();
                push.enabled = true;
                handler.ability1 = push; // Assign to the handler
                break;

            case RoleType.Scout:
                Ability dash = GetComponent<DashAbility>();
                dash.enabled = true;
                handler.ability1 = dash;
                break;

            case RoleType.Support:
                Ability slow = GetComponent<SlowField>();
                slow.enabled = true;
                handler.ability1 = slow;
                break;
        }
    }
}