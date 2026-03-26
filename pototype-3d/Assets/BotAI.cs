using UnityEngine;
using UnityEngine.AI;

public class BotAI : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject[] zones;

    float waitTime = 4f;
    float timer = 0f;
    bool waiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        zones = GameObject.FindGameObjectsWithTag("Zone");
        MoveToRandomZone();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            if (!waiting)
            {
                waiting = true;
                timer = waitTime;
                agent.isStopped = true;
            }
        }

        if (waiting)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                waiting = false;
                agent.isStopped = false;
                MoveToRandomZone();
            }
        }
    }

    void MoveToRandomZone()
    {
        int index = Random.Range(0, zones.Length);
        agent.SetDestination(zones[index].transform.position);
    }
}