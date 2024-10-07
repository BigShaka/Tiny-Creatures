using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Hunter : MonoBehaviour
{
    [Header("stateControl")]
    public State currentState;
    public Transform[] waypoints;
    [Header("stats")]
    public float DrainEnergy = 10f;
    public float restRate = 15f;
    public float Energy = 100f;
    public float Speed = 3f;
    public float Vision = 10f;
    [Header("chase")]
    public Vector3 chaseAreaCenter = Vector3.zero;
    public Vector3 chaseAreaSize = new(30, 0, 30);
    [Header("speedvariable")]
    public float maxSpeed = 5f;
    public float maxForce = 10f;
    public Vector3 velocity;
    [Header("obstacle")]
    public float obstacleRadius = 1f;
    public float obstacleDistance = 5f;
    [Header("referencias")]
    public GameObject target;

    void Start()
    {
        ChangeState(new Patrol());
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Execute(this);
        }

        if (Energy <= 0 && !(currentState is Rest))
        {
            ChangeState(new Rest());
        }
    }
    public void MoveTo(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        transform.position += Speed * Time.deltaTime * direction;
    }
    public void Stop()
    {
        Speed = 0;
    }
    public Vector3 ObstacleAvoidance()
    {
        RaycastHit hit;
        float avoidWeight = 0.5f;
        Debug.DrawRay(transform.position, transform.forward * obstacleDistance, Color.yellow);

        if (Physics.SphereCast(transform.position, obstacleRadius, transform.forward, out hit, obstacleDistance))
        {
            if (hit.collider.CompareTag("obstacle") || hit.collider.CompareTag("food"))
            {
                Debug.Log("Evitando obstaculo: " + hit.collider.name);
                Vector3 ObstacleNormal = hit.normal;
                Vector3 AvoidDirection = Vector3.Cross(ObstacleNormal, Vector3.up);
                Vector3 desired = (AvoidDirection.normalized * maxSpeed) * avoidWeight;
                Debug.DrawRay(transform.position, AvoidDirection * 3, Color.red);

                return Vector3.ClampMagnitude(desired, maxForce);
            }
        }

        return Vector3.zero;
    }
    public GameObject DetectBoids()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, Vision);
        GameObject closestBoid = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("boid"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBoid = hit.gameObject;
                }
            }
        }
        return closestBoid;
    }
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        currentState.Enter(this);
    }

}
