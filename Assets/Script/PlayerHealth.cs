using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Transform respawnPoint;
    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // 🔴 IMPORTANT

        currentHealth -= damage;

        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player Died");

        StartCoroutine(RespawnDelay());
    }
    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(2f);

        Respawn();
    }

    void Respawn()
    {
        Transform spawn = FindAnyObjectByType<RespawnManager>().GetRandomSpawn();

        if (spawn != null)
        {
            transform.position = spawn.position;
        }

        currentHealth = maxHealth;
        isDead = false; // 🔴 RESET
    }
}