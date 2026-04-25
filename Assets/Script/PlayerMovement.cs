using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public Transform cameraTransform;
    public float rotationSpeed = 8f;

    public Gun gun;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    public float gravity = -9.81f;

    bool isShooting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Movement input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Shooting input
    void OnShoot(InputValue value)
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        isShooting = value.isPressed;
    }

    void Update()
    {

        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        Move();

        if (isShooting)
        {
            gun.TryShoot();
        }
    }

    void Move()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

}