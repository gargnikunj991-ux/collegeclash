using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    bool isDead = false;

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            isDead = true;
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}