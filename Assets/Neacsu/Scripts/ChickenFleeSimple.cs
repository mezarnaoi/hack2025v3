using UnityEngine;

public class ChickenFleeSimple : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 5f;
    public float moveSpeed = 3f;
    public float obstacleAvoidDistance = 1f;

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < fleeDistance)
        {
            // Direcția opusă față de jucător
            Vector3 fleeDirection = (transform.position - player.position).normalized;

            // Verificăm dacă avem obstacol în față
            if (Physics.Raycast(transform.position, fleeDirection, obstacleAvoidDistance))
            {
                // Dacă ne blocăm, ne întoarcem un pic
                fleeDirection += transform.right * 0.5f;
            }

            // Mișcăm găina
            transform.Translate(fleeDirection * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = fleeDirection; // se rotește în direcția de fugă
        }
    }
}
