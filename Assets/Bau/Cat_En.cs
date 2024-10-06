using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_En : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla
    public float detectionRadius = 5f; // Radio de detección
    public float jumpForce = 10f; // Fuerza del salto
    public LayerMask playerLayer; // Capa del jugador
    public LayerMask obstacleLayer; // Capa de obstáculos
    public Transform laserImpactPoint; // Punto de impacto del láser
    public float laserAttractionRadius = 10f; // Radio de atracción del láser

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentPatrolPoint = 0;
    private bool isChasingPlayer = false;
    private Vector3 lastSeenPlayerPosition;
    private bool isLaserActive = false;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    private void Update()
    {
        if (isLaserActive)
        {
            GoToLaserImpactPoint();
        }
        else if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            Vector3 direction = hitCollider.transform.position - transform.position;
            if (!Physics.Raycast(transform.position, direction, detectionRadius, obstacleLayer))
            {
                isChasingPlayer = true;
                lastSeenPlayerPosition = hitCollider.transform.position;
                agent.SetDestination(lastSeenPlayerPosition);
                break;
            }
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, lastSeenPlayerPosition) < 2f)
        {
            JumpToPlayer();
        }
        else
        {
            agent.SetDestination(lastSeenPlayerPosition);
        }
    }

    private void JumpToPlayer()
    {
        Vector3 jumpDirection = (lastSeenPlayerPosition - transform.position).normalized;
        agent.ResetPath();
        GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
    }

    private void GoToLaserImpactPoint()
    {
        agent.SetDestination(laserImpactPoint.position);
    }

    public void ActivateLaser(Transform impactPoint)
    {
        isLaserActive = true;
        laserImpactPoint = impactPoint;
    }

    public void DeactivateLaser()
    {
        isLaserActive = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}