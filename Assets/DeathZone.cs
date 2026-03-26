using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Respawn respawn = other.GetComponent<Respawn>();

        if (respawn != null)
        {
            StartCoroutine(respawn.RespawnPlayer());
        }
    }
}
