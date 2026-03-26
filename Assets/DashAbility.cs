using UnityEngine;

public class DashAbility : Ability
{
    public float dashForce = 20f;

    protected override void Activate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
    }
}