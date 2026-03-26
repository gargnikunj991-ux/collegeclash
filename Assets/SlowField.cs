using UnityEngine;

public class SlowField : Ability
{
    public float radius = 5f;

    protected override void Activate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerMovement m = hit.GetComponent<PlayerMovement>();
                m.speed *= 0.5f;

                StartCoroutine(ResetSpeed(m));
            }
        }
    }

    System.Collections.IEnumerator ResetSpeed(PlayerMovement m)
    {
        yield return new WaitForSeconds(2f);
        m.speed *= 2f;
    }
}