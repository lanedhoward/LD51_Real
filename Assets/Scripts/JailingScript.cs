using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailingScript : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private int currentWaypoint = 0;

    void Update()
    {
        bool check;
        if(player.interactionsInventory.inventory.TryGetValue("tookRobbersGun", out check) && check == true)
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
