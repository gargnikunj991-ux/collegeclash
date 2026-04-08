using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;
    float damageCooldown = 1f;
    float lastHitTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
        {
            return;
        }
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= lastHitTime + damageCooldown)
            {
                PlayerHealth player = other.GetComponent<PlayerHealth>();

                if (player != null)
                {
                    player.TakeDamage(20);
                    lastHitTime = Time.time;
                }
            }
        }
    }
}