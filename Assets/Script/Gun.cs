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

    private AudioSource audioSource; // ✔ correct place

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // ✔ assign once
    }

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

        // recoil
        if (gunVisual != null)
        {
            gunVisual.localPosition -= new Vector3(0, 0, recoilAmount);
        }

        // 🔊 PLAY SOUND
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // shoot from center
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)

        );
        Vector3 spread = new Vector3(
    Random.Range(-0.02f, 0.02f),
    Random.Range(-0.02f, 0.02f),
    0f
);

        ray.direction += spread;

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.3f);

        int layerMask = ~LayerMask.GetMask("Zone");

        if (Physics.Raycast(ray, out RaycastHit hit, range, layerMask))
        {
            Debug.Log("Hit: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage((int)damage);
                if (HitMarkerManager.Instance != null)
                    HitMarkerManager.Instance.ShowHit();

                if (HitSound.Instance != null)
                    HitSound.Instance.Play();
            
        }
        }
    }
}