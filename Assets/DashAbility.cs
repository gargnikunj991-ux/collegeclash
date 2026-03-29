using UnityEngine;

public class DashAbility : Ability
{
    public float dashForce = 20f;
    public float dashDuration = 0.2f;

    private CharacterController controller;
    private PlayerMovement movement;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    protected override void Activate()
    {
        // Logic for the dash
        Vector3 dashDir = transform.forward * dashForce;
        StartCoroutine(PerformDash(dashDir));
    }

    private System.Collections.IEnumerator PerformDash(Vector3 direction)
    {
        float timer = 0;
        while (timer < dashDuration)
        {
            controller.Move(direction * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}