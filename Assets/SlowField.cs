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
                if (m != null) StartCoroutine(ApplySlow(m));
            }
        }
    }

    IEnumerator ApplySlow(PlayerMovement m)
    {
        float oldSpeed = m.speed;
        m.speed *= 0.5f;
        yield return new WaitForSeconds(slowDuration);
        m.speed = oldSpeed;
    }
}