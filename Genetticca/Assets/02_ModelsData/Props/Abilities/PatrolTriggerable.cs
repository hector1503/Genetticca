using UnityEngine;
using System.Collections;

public class PatrolTriggerable : MonoBehaviour
{
    [HideInInspector]
    public Patrol patrol;
    [HideInInspector]
    public Transform[] Waypoints;

    Vector3 TargetWayPoint;
    Vector3 MoveDirection;
    Vector3 Velocity;

    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (patrol.doPatrol)
            PatrolRefresh();
    }


    public void SetActivePatrol(bool value)
    {
        patrol.doPatrol = value;

    }

   

    private void PatrolRefresh()
    {
        if (patrol.curWayPoint < Waypoints.Length)
        {
            TargetWayPoint = Waypoints[patrol.curWayPoint].position;
            MoveDirection = TargetWayPoint - transform.position;
            Velocity = rb.velocity;

            if (MoveDirection.magnitude < 1)
            {
                patrol.curWayPoint++;
            }
            else
            {
                Velocity = MoveDirection.normalized * patrol.speed;
            }
        }
        else
        {
            if (patrol.doPatrol)
            {
                patrol.curWayPoint = 0;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }

        rb.velocity = Velocity * Time.deltaTime;
    }
}

