using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    float damageCooldown = 1f;
    float lastHitTime = 0f;

    private Vector3 originalScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Time.time >= lastHitTime + damageCooldown)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
                lastHitTime = Time.time;

                // HIT EFFECT (only when damage happens)
                HitEffect();
            }
        }
    }

    void HitEffect()
    {
        transform.localScale = originalScale * 0.9f;

        CancelInvoke(nameof(ResetScale));
        Invoke(nameof(ResetScale), 0.1f);
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }
}