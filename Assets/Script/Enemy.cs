using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;
    enum EnemyState
    {
        Chase,
        Attack
    }

    EnemyState currentState;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;
    public float stoppingDistance = 2.5f;

    float damageCooldown = 1.5f;
    float lastHitTime = 0f;

    private Vector3 originalScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = transform.localScale;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (attackRange >= stoppingDistance)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Attack;
        }

        if (currentState == EnemyState.Chase)
        {
            MoveTowardsPlayer(distance);
        }
        else if (currentState == EnemyState.Attack)
        {
            TryAttack();
        }
    }
    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }
    void Attack()
    {
        Debug.Log("Enemy attacked");

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }
    }
    void MoveTowardsPlayer(float distance)
    {
        if (distance > stoppingDistance)
        {
            Vector3 offset = new Vector3(
                Mathf.Sin(Time.time + transform.GetInstanceID()) * 0.5f,
                0,
                Mathf.Cos(Time.time + transform.GetInstanceID()) * 0.5f
            );

            Vector3 targetPosition = player.position + offset;

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
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