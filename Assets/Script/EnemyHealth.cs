using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    Renderer rend;
    Color originalColor;

    bool isDead = false;

    Vector3 originalScale; // ✔ store original

    void Start()
    {
        currentHealth = maxHealth;

        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // SCALE HIT
        transform.localScale = originalScale * 0.8f;
        Invoke(nameof(ResetScale), 0.1f);

        // COLOR FLASH (ADD THIS HERE)
        if (rend != null)
        {
            rend.material.color = Color.red;
            Invoke(nameof(ResetColor), 0.1f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void ResetColor()
    {
        if (rend != null)
        {
            rend.material.color = originalColor;
        }
    }

    void ResetScale()
    {
        transform.localScale = originalScale; // ✔ restore properly
    }

    void Die()
    {
        isDead = true;

        Debug.Log("Enemy Died");

        Destroy(gameObject);
    }
}