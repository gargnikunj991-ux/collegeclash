using UnityEngine;
using System.Collections;

public class SlowField : Ability
{
    public float radius = 5f;
    public float slowDuration = 2f;

    protected override void Activate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerMovement m = hit.GetComponent<PlayerMovement>();
                if (m != null)
                {
                    StartCoroutine(ApplySlowEffect(m));
                }
            }
        }
    }

    IEnumerator ApplySlowEffect(PlayerMovement m)
    {
        float originalSpeed = m.speed;
        m.speed = originalSpeed * 0.5f;

        yield return new WaitForSeconds(slowDuration);

        // Reset to exactly the original speed to prevent multiplier stacking
        m.speed = originalSpeed;
    }
}