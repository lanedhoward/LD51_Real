using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveMover : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private int currentWaypoint = 0;

    void Update()
    {
        if(currentWaypoint < waypoints.Length)
        {
            CheckPosition();
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed);
        }
        
    }

    void CheckPosition()
    {
        if(transform.position == waypoints[currentWaypoint].position)
        {
            currentWaypoint++;
        }
    }
}
