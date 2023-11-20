using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Drones_Job : MonoBehaviour
{
    private float moveSpeed;
    private float rotationSpeed;
    private bool isMovingTowradsPlayer;
    private Vector3 movement;
    private Rigidbody rb;
    [SerializeField] private Transform player;

    [Header("Patrolling!:")]
    [SerializeField] GameObject patrolPoint;
    [SerializeField] float patrolPointRange;
    [SerializeField] float patrolSpeedmultiplier;
    [SerializeField] float patrolwaypointAcceptaceRadius;
    private Vector3 waypoint;
    private bool isWaypointSet = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 10f;
        rotationSpeed = 10.0f;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // StartCoroutine(SelfDestruction());
        //transform.LookAt(player.position);
        
       // Patrol();
       StartCoroutine(MoveTowardsPlayer());
       
        //transform.position += player.position * Time.deltaTime;
       

    }
    void MoveToRandomDirections()
    {
        Vector3 moveDirection = transform.position - player.position;
        float moveXdirection = moveDirection.x + Mathf.Sin(3f * 1f);
        float moveYdirection = moveDirection.y + Mathf.Sin(2f * 3f); ;
        float moveZdirection = moveDirection.z + Mathf.Sin(3f * 1f);

        Vector3 addingMovement = new Vector3(moveXdirection, moveYdirection, moveZdirection);
        transform.Translate(addingMovement * Time.deltaTime);
    }
    private void Patrol()
    {
        if (!isWaypointSet) FindWayPoint();
        if (isWaypointSet)
        {
            transform.LookAt(waypoint);
            rb.AddRelativeForce(Vector3.forward * patrolSpeedmultiplier, ForceMode.Force);

        }

        WayPointReachedCheck();
    }

    private void WayPointReachedCheck()
    {
        Vector3 distanceToWalkPoint = transform.position - waypoint;
        if (distanceToWalkPoint.magnitude < patrolwaypointAcceptaceRadius)
        {
            isWaypointSet = false;
        }
    }
    IEnumerator MoveTowardsPlayer()
    {
        float seconds = 3f;

        yield return new WaitForSeconds(seconds);
        isMovingTowradsPlayer = true;
        Vector3 moveDirection = player.position - transform.position;
        Quaternion moveRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, rotationSpeed * Time.deltaTime);
        rb.MovePosition(transform.position += (moveDirection * 5f * Time.deltaTime));
    }
    IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(15f);   
        gameObject.SetActive(false);
    }

    private void FindWayPoint()
    {
        float randomZ = Random.Range(-patrolPointRange, patrolPointRange);
        float randomX = Random.Range(-patrolPointRange, patrolPointRange);

        Vector3 currentPos = patrolPoint.transform.position;
        waypoint = new Vector3(currentPos.x + randomX, currentPos.y, currentPos.z + randomZ);
        isWaypointSet = true;
    }
}
