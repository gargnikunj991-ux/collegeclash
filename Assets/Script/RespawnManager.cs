using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public Transform GetRandomSpawn()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return null;
        }

        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
}