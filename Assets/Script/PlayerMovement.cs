using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public Transform cameraTransform;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private CharacterController controller;
    private Vector2 moveInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Movement input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Shoot input
    void OnShoot()
    {
        Shoot();
    }

    void Update()
    {
        Move();
        RotateToMouse();
    }

    void Move()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * moveInput.y + camRight * moveInput.x;

        controller.Move(move * speed * Time.deltaTime);
    }
    void RotateToMouse()
    {
        if (Mouse.current == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float rayLength))
        {
            Vector3 point = ray.GetPoint(rayLength);
            Vector3 direction = point - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
    }
}