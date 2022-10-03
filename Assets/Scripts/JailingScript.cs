using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailingScript : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float speed;
    private CapsuleCollider2D playerCollider;
    private int currentWaypoint = 0;
    private bool timeToMovePlayer;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        waypoints[0] = player.transform;
    }
    void Update()
    {
        bool check;
        if(player.interactionsInventory.inventory.TryGetValue("jailed", out check) && check == true)
        {
            if(currentWaypoint < waypoints.Length)
            {
                CheckPosition();
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed);
                if(timeToMovePlayer)
                {
                    playerCollider.enabled = false;
                    player.enabled =false;
                    player.transform.position = Vector3.MoveTowards(player.transform.position, waypoints[1].position, speed);
                }
            }
            
        }
    }
    void CheckPosition()
    {
        if(this.transform.position == waypoints[currentWaypoint].position)
        {
            currentWaypoint++;
        }
        if(transform.position == player.transform.position)
        {
            timeToMovePlayer = true;
        }
        if(player.transform.position == waypoints[1].position)
        {
            player.enabled = true;
            playerCollider.enabled = true;
            timeToMovePlayer = false;
            Debug.Log("proof");
        }
    }
}
