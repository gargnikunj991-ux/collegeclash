using UnityEngine;
using System.Collections;

public class DashAbility : Ability
{
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    protected override void Activate()
    {
        StartCoroutine(PerformDash());
    }

    IEnumerator PerformDash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            // Moves the player forward quickly
            controller.Move(transform.forward * dashForce * Time.deltaTime);
            yield return null;
        }
    }
}