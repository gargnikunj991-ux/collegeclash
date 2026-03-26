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
}
