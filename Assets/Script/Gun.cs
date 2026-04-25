using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Header("Setup")]
    public Transform firePoint;
    public Transform gunVisual;

    [Header("Shooting")]
    public float fireRate = 0.2f;
    public float damage = 20f;
    public float range = 100f;

    [Header("Recoil")]
    public float recoilAmount = 0.1f;
    public float recoilRecoverSpeed = 10f;

    private float nextFireTime = 0f;
    private bool isShooting = false;

    void Update()
    {
        // recoil recovery
        if (gunVisual != null)
        {
            gunVisual.localPosition = Vector3.Lerp(
                gunVisual.localPosition,
                Vector3.zero,
                recoilRecoverSpeed * Time.deltaTime
            );
        }

        // shooting loop
        if (isShooting)
        {
            TryShoot();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
            isShooting = true;

        if (context.canceled)
            isShooting = false;
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        Shoot();
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint not assigned!");
            return;
        }

        // recoil kick
        if (gunVisual != null)
        {
            gunVisual.localPosition -= new Vector3(0, 0, recoilAmount);
        }
        Ray ray = Camera.main.ScreenPointToRay(
    new Vector3(Screen.width / 2, Screen.height / 2)
);

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage((int)damage);

                if (HitMarkerManager.Instance != null)
                {
                    HitMarkerManager.Instance.ShowHit();
                }
            }
        }
    }
}