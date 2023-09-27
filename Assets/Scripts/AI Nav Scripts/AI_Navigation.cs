using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Navigation : MonoBehaviour
{
    //Way point to create AI nav path
    private GameObject waypointsPrefab;

    //Floating variables
    private Vector3 InitialPosition;
    private float floatingHorizontalOffset = 0.0f;
    private float floatingHorizontalDistance = 0.3f;
    private float verticalPositionOffset;
    private float floatingHeight = 0.3f;
    private float floatSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FloatingAI();
       // AIGroundLevelCheck();
    }

    private void FloatingAI()
    {
        //Floating calculation 
        verticalPositionOffset = Mathf.Sin(Time.time * floatSpeed) * floatingHeight;
        floatingHorizontalOffset = Mathf.Sin(Time.time * floatSpeed * 0.5f) * floatingHorizontalDistance;

        Vector3 newPosition = InitialPosition + Vector3.up * verticalPositionOffset + Vector3.right * floatingHorizontalOffset;
        transform.position = newPosition;

    }
    private void AIGroundLevelCheck()
    {
        Ray rayToGround;
        RaycastHit hitGround;
        float groundCheckOffset = 2.0f;

        Debug.DrawLine(transform.position + Vector3.up * 10f, Vector3.down, Color.red);

        rayToGround = new Ray(transform.position + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(rayToGround, out hitGround, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
            float heightTarget = hitGround.point.y + groundCheckOffset;
            transform.position = new Vector3(transform.position.x, heightTarget, transform.position.z);
        }
        
    }
}
