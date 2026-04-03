using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;
    }
}