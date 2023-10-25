using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Drones : MonoBehaviour
{
    //Way point to create AI nav path
    private GameObject waypointsPrefab;

    public float moveSpeed = 5f;
    Transform player; // Reference to the player's transform

    private bool isMoving = false;

    //Floating variables
    //private Vector3 InitialPosition;
    //private float floatingHorizontalOffset = 0.0f;
    //private float floatingHorizontalDistance = 0.3f;
    //private float verticalPositionOffset;
    //private float floatingHeight = 0.3f;
    //private float floatSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //InitialPosition = transform.position;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //FloatingAI();
        // AIGroundLevelCheck();

        //if (!isMoving)
        //{
            // Move towards the player if not already moving
        MoveTowardsPlayer();
        AIGroundLevelCheck();
            //transform.LookAt(player.transform.position);
        //}
    }

    //private void FloatingAI()
    //{
    //    //Floating calculation 
    //    verticalPositionOffset = Mathf.Sin(Time.time * floatSpeed) * floatingHeight;
    //    floatingHorizontalOffset = Mathf.Sin(Time.time * floatSpeed * 0.5f) * floatingHorizontalDistance;

    //    Vector3 newPosition = InitialPosition + Vector3.up * verticalPositionOffset + Vector3.right * floatingHorizontalOffset;
    //    transform.position = newPosition;

    //}
    private void AIGroundLevelCheck()
    {
        Ray rayToGround;
        RaycastHit hitGround;
        float groundCheckOffset = 3.0f; //Height of the drone above the ground

        Debug.DrawLine(transform.position + Vector3.up * 0f, Vector3.down, Color.red);

        rayToGround = new Ray(transform.position + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(rayToGround, out hitGround, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            float heightTarget = hitGround.point.y + groundCheckOffset;
            transform.position = new Vector3(transform.position.x, heightTarget, transform.position.z);
        }

    }

    void MoveTowardsPlayer()
    {
        isMoving = true;

        // Calculate the direction to the player
        Vector3 direction = (transform.position - player.position).normalized;

        // Move towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the unit has reached close to the player
        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            // Stop moving
            isMoving = false;
        }
    }
}
