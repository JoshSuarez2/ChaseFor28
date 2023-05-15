using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;//Array in case more waypoints are planned
    private int currWaypointIndex = 0; //Where its heading towards
    [SerializeField] private float speed = 2f;
    private float arrived = .1f;

    private void Update()
    {
        //At waypoint, time to move to next
        if (Vector2.Distance(waypoints[currWaypointIndex].transform.position, transform.position) < arrived) 
        {
            currWaypointIndex++;
            if(currWaypointIndex >= waypoints.Length)
            {
                currWaypointIndex = 0;
            }
        
        
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currWaypointIndex].transform.position, Time.deltaTime * speed);

    }
}
