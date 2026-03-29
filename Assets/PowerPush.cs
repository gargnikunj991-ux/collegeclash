using UnityEngine;

public class PowerPush : Ability
{
    public float pushForce = 50f;
    public float pushRadius = 5f;

    protected override void Activate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("BotAI"))
            {
                Rigidbody botRb = hitCollider.GetComponent<Rigidbody>();
                if (botRb != null)
                {
                    Vector3 pushDir = (hitCollider.transform.position - transform.position).normalized;
                    pushDir.y = 0.2f; // Slight lift
                    botRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                }
            }
        }
    }
}