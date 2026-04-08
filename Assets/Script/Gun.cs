using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    bool isShooting = false;

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
        }

        if (context.canceled)
        {
            isShooting = false;
        }
    }

    void Update()
    {
        if (isShooting)
        {
            TryShoot(); // ✅ FIXED
        }
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        Shoot();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}