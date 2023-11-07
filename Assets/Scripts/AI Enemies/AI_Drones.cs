using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.PlayerLoop;
using Valve.VR.InteractionSystem;

public class AI_Drones : MonoBehaviour
{
    private float moveSpeed = 5f;
    public float rotationSpeed = 10.0f;
    public float detectPlayerRadius = 1.0f;
    public float patrolChangeInterval = 5f; // Time interval to change patrol direction
    private float lastPatrolChangeTime;
    private Vector3 patrolDirection; // Current patrol direction
    Vector3 movement;
    Transform player; // Reference to the player's transform
    Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Initialize patrol direction
        patrolDirection = GetRandomDirection();
        lastPatrolChangeTime = Time.time;
    }

    void Update()
    {
            StartCoroutine(MoveToPlayer());
       
            // Patrol behavior: change patrol direction at intervals
        if (Time.time - lastPatrolChangeTime <= patrolChangeInterval)
        {
            patrolDirection = GetRandomDirection();
            lastPatrolChangeTime = Time.time;
            
        }

        // Move in the current patrol direction
        movement = patrolDirection.normalized;// This give a weird shake to the drones


    }
     IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(2);
        Vector3 moveDirection = player.position - transform.position;
        if (moveDirection.magnitude >= detectPlayerRadius)
        {
            moveSpeed = 10f;
            // Rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move towards the player
            movement = moveDirection.normalized;
        }

    }
    private void FixedUpdate()
    {
        MoveDroneTowardsPlayer(movement);
    }

    void MoveDroneTowardsPlayer(Vector3 direction)
    {
        // Move the drone using rigidbody
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    Vector3 GetRandomDirection()
    {
        // Get a random direction for patrolling
        float randomX = Random.Range(-1f, 15f);
        float randomZ = Random.Range(-1f, 15f);
        float randomY = Random.Range(-1f, 5f);

        return new Vector3(randomX, randomY, randomZ).normalized;
    }








    ////Way point to create AI nav path

    //private float moveSpeed = 5f;
    //public float rotationSpeed = 3.0f;
    //public float detectPlayerRadius = 5.0f;
    //public float wayPointDistance = 0;
    //Transform player; // Reference to the player's transform
    //Vector3 movement;

    //Rigidbody rb;


    //void Start()
    //{
    //    //InitialPosition = transform.position;
    //    rb = this.GetComponent<Rigidbody>();

    //    if (player == null)
    //    {
    //        player = GameObject.FindGameObjectWithTag("Player").transform;
    //    }
    //}

    //void Update()
    //{
    //    Vector3 moveDirection = player.position - transform.position;
    //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    //    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //    rb.rotation = rotation;
    //    moveDirection.Normalize();
    //    movement = moveDirection;

    //    transform.LookAt(player.transform.position);
    //}
    //private void FixedUpdate()
    //{
    //    MoveDroneTowardsPlayer(movement);
    //}

    //void MoveDroneTowardsPlayer(Vector3 direction) {

    //    rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    //}

}




