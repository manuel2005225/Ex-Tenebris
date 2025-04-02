using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;     // Puntos de patrullaje
    public float lightDetectionTime = 1f; // Tiempo necesario de exposición a la luz
    public float detectionRadius = 10f;
    public Transform player;

    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private float lightTimer = 0f;
    private bool isChasing = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
            return;
        }

        // Patrullaje
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            lightTimer += Time.deltaTime;

            if (lightTimer >= lightDetectionTime)
            {
                isChasing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerLight"))
        {
            lightTimer = 0f;
        }
    }
}