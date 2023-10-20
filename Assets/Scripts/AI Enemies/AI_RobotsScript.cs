using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RobotsScript : MonoBehaviour
{
    public float moveSpeed = 25f;
    public Transform player; // Reference to the player's transform

    private bool isMoving = false;

    void Start()
    {
        // Find the player object in the scene if not assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            // Move towards the player if not already moving
            MoveTowardsPlayer();
            //transform.LookAt(player.transform.position);
        }
    }

    void MoveTowardsPlayer()
    {
        isMoving = true;

        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the unit has reached close to the player
        if (Vector3.Distance(transform.position, player.position) > 1f)
        {
            // Stop moving
            isMoving = false;
        }
    }








    /* GameObject player;
     // Start is called before the first frame update
     void Start()
     {
         player = GameObject.FindGameObjectWithTag("Player");
     }

     // Update is called once per frame
     void Update()
     {
         transform.LookAt(player.transform.position);*/
    //}
}
