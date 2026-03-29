using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Get Input
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // 2. Handle Movement
        if (moveDirection.magnitude >= 0.1f)
        {
            // Rotate toward movement direction
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Using 'speed' here to match your other scripts
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        // 3. Handle Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}