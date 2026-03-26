using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Transform spawnPoint;

    public IEnumerator RespawnPlayer()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        transform.position = spawnPoint.position;

        gameObject.SetActive(true);
    }
}