using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public override void Enter(Hunter Cazador)
    {
        Debug.Log("Entrando en estado de persecucion");
    }

    public override void Execute(Hunter Cazador)
    {
        if (Cazador.Energy <= 0)
        {
            Cazador.ChangeState(new Rest());
            return;
        }

        Cazador.Energy -= Cazador.DrainEnergy * Time.deltaTime;

        if (Cazador.target == null)
        {
            GameObject newTarget = FindNewBoidInChaseArea(Cazador);
            if (newTarget != null)
            {
                Cazador.target = newTarget;
                Debug.Log("Boid perdido, cambiando objetivo a otro Boid dentro del área.");
            }
            else
            {
                Cazador.ChangeState(new Patrol());
            }
            return;
        }

        if (!IsBoidInChaseArea(Cazador, Cazador.target.transform.position))
        {
            GameObject newTarget = FindNewBoidInChaseArea(Cazador);
            if (newTarget != null)
            {
                Cazador.target = newTarget;
                Debug.Log("Cambiando objetivo a otro Boid dentro del área.");
            }
            else
            {
                Cazador.target = null;
                Cazador.ChangeState(new Patrol());
                return;
            }
        }

        Vector3 pursuitDirection = (Cazador.target.transform.position - Cazador.transform.position).normalized;
        Vector3 avoidanceForce = Cazador.ObstacleAvoidance();
        Vector3 combinedDirection = pursuitDirection + avoidanceForce;
        Cazador.MoveTo(Cazador.transform.position + combinedDirection);
    }

    public override void Exit(Hunter cazador)
    {
        Debug.Log("Saliendo del estado de persecucion");
    }

    private GameObject FindNewBoidInChaseArea(Hunter Cazador)
    {
        Collider[] hits = Physics.OverlapSphere(Cazador.transform.position, Cazador.Vision);
        GameObject closestBoid = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("boid"))
            {
                Vector3 boidPosition = hit.transform.position;
                if (IsBoidInChaseArea(Cazador, boidPosition))
                {
                    float distance = Vector3.Distance(Cazador.transform.position, boidPosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestBoid = hit.gameObject;
                    }
                }
            }
        }
        return closestBoid;
    }

    private bool IsBoidInChaseArea(Hunter Cazador, Vector3 boidPosition)
    {
        Vector3 offset = boidPosition - Cazador.chaseAreaCenter;
        return Mathf.Abs(offset.x) <= Cazador.chaseAreaSize.x / 2 &&
               Mathf.Abs(offset.z) <= Cazador.chaseAreaSize.z / 2;
    }
}

