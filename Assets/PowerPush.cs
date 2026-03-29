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
            // Check for Rigidbody first to avoid nested if statements
            Rigidbody botRb = hitCollider.GetComponent<Rigidbody>();

            if (botRb != null && hitCollider.gameObject.layer == LayerMask.NameToLayer("BotAI"))
            {
                Vector3 pushDir = (hitCollider.transform.position - transform.position).normalized;
                pushDir.y = 0.1f;

                botRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                Debug.Log("Pushed: " + hitCollider.name);
            }
        }
    }
}