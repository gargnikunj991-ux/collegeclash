using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0f;

        Instantiate(enemyPrefab, randomPos, Quaternion.identity);
    }
}