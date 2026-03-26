using UnityEngine;

public enum Role
{
    Brawler,
    Controller,
    Disruptor
}

public class PlayerRole : MonoBehaviour
{
    public Role role;

    private PlayerMovement movement;
    private PlayerCombat combat;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();

        Debug.Log("PlayerRole Start Running"); // DEBUG

        ApplyRoleStats();
        AssignAbility();
    }

    void ApplyRoleStats()
    {
        switch (role)
        {
            case Role.Brawler:
                movement.speed = 4f;
                combat.pushForce = 15f;
                break;

            case Role.Controller:
                movement.speed = 3f;
                combat.pushForce = 8f;
                break;

            case Role.Disruptor:
                movement.speed = 6f;
                combat.pushForce = 6f;
                break;
        }
    }

    void AssignAbility()
    {
        Debug.Log("Assigning ability for: " + role);

        switch (role)
        {
            case Role.Brawler:
                gameObject.AddComponent<PowerPush>();
                break;

            case Role.Controller:
                gameObject.AddComponent<SlowField>();
                break;

            case Role.Disruptor:
                gameObject.AddComponent<DashAbility>();
                break;
        }
    }
}