using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 6f;
    public float fleeSpeed = 4f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fleeSpeed;
        agent.baseOffset = 0f;

        // Dacă player nu e setat manual, îl caută după nume
        if (player == null)
        {
            GameObject p = GameObject.Find("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null || agent == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < fleeDistance)
        {
            // Direcția OPUȘĂ față de jucător
            Vector3 awayFromPlayer = (transform.position - player.position).normalized;

            // Target = poziția curentă + o direcție înapoi
            Vector3 fleeTarget = transform.position + awayFromPlayer * 5f;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleeTarget, out hit, 3f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        {
            // Dacă player nu e aproape, stă pe loc
            agent.ResetPath();
        }
    }
}
