using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    public int damage = 25;
    public int maxHits = 3;
    public float damageFalloff = 0.5f; // 50% reduction each hit

    private int hitCount = 0;
    private List<GameObject> hitEnemies = new List<GameObject>();

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (hitEnemies.Contains(other.gameObject)) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null) return;

        // Apply damage with falloff
        int currentDamage = Mathf.RoundToInt(damage * Mathf.Pow(damageFalloff, hitCount));
        enemy.TakeDamage(currentDamage);

        hitEnemies.Add(other.gameObject);
        hitCount++;

        if (hitCount >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}