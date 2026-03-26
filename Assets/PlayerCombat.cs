using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float pushForce = 8f;
    public float pushCooldown = 0.5f;

    private float lastPushTime;

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (Time.time < lastPushTime + pushCooldown) return;

        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

        Vector3 direction = (collision.transform.position - transform.position).normalized;

        // Flatten direction (no vertical weirdness)
        direction.y = 0;

        otherRb.linearVelocity = Vector3.zero; // reset weird momentum
        otherRb.AddForce(direction * pushForce, ForceMode.Impulse);

        lastPushTime = Time.time;
    }
}