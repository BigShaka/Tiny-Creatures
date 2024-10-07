using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
    private int currentWaypointIndex = 0;

    public override void Enter(Hunter npc)
    {
        Debug.Log("Entro en estado patrullaje");
    }

    public override void Execute(Hunter cazador)
    {
        if (cazador.Energy <= 0)
        {
            cazador.ChangeState(new Rest());
            return;
        }

        cazador.Energy -= cazador.DrainEnergy * Time.deltaTime;

        if (cazador.waypoints.Length == 0) return;
        Transform targetWaypoint = cazador.waypoints[currentWaypointIndex];
        Vector3 moveDirection = (targetWaypoint.position - cazador.transform.position).normalized;
        Vector3 obstacleAvoidanceForce = cazador.ObstacleAvoidance();

        if (obstacleAvoidanceForce != Vector3.zero)
        {
            moveDirection = (moveDirection + obstacleAvoidanceForce).normalized;
        }
        cazador.MoveTo(cazador.transform.position + moveDirection * cazador.Speed * Time.deltaTime);
        if (Vector3.Distance(cazador.transform.position, targetWaypoint.position) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % cazador.waypoints.Length;
        }
        GameObject closestBoid = cazador.DetectBoids();
        if (closestBoid != null && IsBoidInChaseArea(cazador, closestBoid.transform.position))
        {
            cazador.target = closestBoid;
            cazador.ChangeState(new Chase());
        }
    }

    public override void Exit(Hunter cazador)
    {
        Debug.Log("Salio del estado patrullaje");
    }

    private bool IsBoidInChaseArea(Hunter cazador, Vector3 boidPosition)
    {
        Vector3 offset = boidPosition - cazador.chaseAreaCenter;
        return Mathf.Abs(offset.x) <= cazador.chaseAreaSize.x / 2 &&
               Mathf.Abs(offset.z) <= cazador.chaseAreaSize.z / 2;
    }
}
