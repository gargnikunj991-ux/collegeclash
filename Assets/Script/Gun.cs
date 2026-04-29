using NUnit.Framework.Internal;
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
    public AudioClip[] shootSounds;
    private Camera cam;
    public GameObject hitEffect; // particle effect 
    public float baseSpread = 0.01f;
    public float movingSpread = 0.04f; // add bullet spread
    public float shootingSpread = 0.03f;
    public GameObject enemyHitEffect;
    public GameObject wallHitEffect;
    private AudioSource audioSource; // ✔ correct place

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
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
            gunVisual.localPosition -= new Vector3(
          Random.Range(-recoilAmount * 0.5f, recoilAmount * 0.5f),
          Random.Range(recoilAmount * 0.5f, recoilAmount),
          recoilAmount
            );
        }
        // gun sound 
        if (audioSource != null && shootSounds.Length > 0)
        {
            audioSource.PlayOneShot(
                shootSounds[Random.Range(0, shootSounds.Length)]
            );
        }

        Ray ray = cam.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );
        float currentSpread = isShooting ? shootingSpread : baseSpread;

        Vector3 spread = new Vector3(
            Random.Range(-currentSpread, currentSpread),
            Random.Range(-currentSpread, currentSpread),
            0f
        );

        Vector3 direction = ray.direction;
        direction += spread;
        direction.Normalize();
        ray.direction = direction;

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.3f);


        int layerMask = ~LayerMask.GetMask("Zone");

        if (Physics.Raycast(ray, out RaycastHit hit, range, layerMask))
        {
            Debug.Log("HIT: " + hit.collider.name);
            HandleHit(hit);
        }
        else
        {
            Debug.Log("MISS");
        }


    }
    // manage hit 
    void HandleHit(RaycastHit hit)
    {
        EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            if (enemyHitEffect != null)
                Instantiate(enemyHitEffect, hit.point + hit.normal * 0.02f, Quaternion.LookRotation(hit.normal));
            Debug.Log("Spawning enemy effect");

            if (HitMarkerManager.Instance != null)
                HitMarkerManager.Instance.ShowHit();

            if (HitSound.Instance != null)
                HitSound.Instance.Play();

            enemy.TakeDamage((int)damage);
        }
        else
        {
            if (wallHitEffect != null)
                Instantiate(wallHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Debug.Log("Spawning wall effect");
        }
    }
}


