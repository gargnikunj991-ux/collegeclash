using UnityEngine;

public class PowerPush : Ability
{
    public float boost = 20f;

    protected override void Activate()
    {
        PlayerCombat combat = GetComponent<PlayerCombat>();
        combat.pushForce *= 2f;

        Invoke(nameof(ResetPush), 2f);
    }

    void ResetPush()
    {
        PlayerCombat combat = GetComponent<PlayerCombat>();
        combat.pushForce /= 2f;
    }
   
}