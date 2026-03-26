using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);
        rb.linearVelocity = move * speed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ability ability = GetComponent<Ability>();
            if (ability != null)
            {
                ability.TryUse();
            }
        }
    }


    public float pushForce = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 dir = collision.transform.position - transform.position;
            rb.AddForce(dir.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
