using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public float dashForce = 30f;
    public float dashCooldown = 2f;
    private float nextDashTime;

    public float pushForce = 8f;
    public float pushRadius = 3f;
    public float pushCooldown = 3f;
    private float nextPushTime;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleDash();
        if (Input.anyKeyDown)
            Debug.Log("KEY PRESSED"); 
        HandlePush();
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime)
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            nextDashTime = Time.time + dashCooldown;
        }
    }

    void HandlePush()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextPushTime)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, pushRadius);

            foreach (var hit in hits)
            {
                if (hit.gameObject == gameObject) continue;

                Rigidbody otherRb = hit.GetComponent<Rigidbody>();
                if (otherRb != null)
                {
                    Vector3 dir = (hit.transform.position - transform.position).normalized;
                    otherRb.AddForce(dir * pushForce, ForceMode.Impulse);
                }
            }

            nextPushTime = Time.time + pushCooldown;
        }
    }
}