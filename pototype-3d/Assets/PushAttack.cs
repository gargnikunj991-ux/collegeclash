using UnityEngine;

public class PushAttack : MonoBehaviour
{
    public float pushForce = 10f;
    public float range = 2f;
    public float cooldown = 2f;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timer >= cooldown)
        {
            timer = 0f;
            Push();
        }
    }

    void Push()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(transform.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}
