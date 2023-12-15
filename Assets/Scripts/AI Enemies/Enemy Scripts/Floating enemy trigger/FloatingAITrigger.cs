using UnityEngine;

public class FloatingAITrigger
{
    //Transform transform;
    private float floatingHorizontalDistance = 0.3f;
    private float floatingHorizontalOffset = 0.0f;
    private float verticalPositionOffset;
    private float floatingHeight = 0.3f;
    private float floatSpeed = 1.0f;

    public void FloatingAI(Vector3 InitialPosition, Transform transform)
    {

        //Floating calculation 
        verticalPositionOffset = Mathf.Sin(Time.time * floatSpeed) * floatingHeight;
        floatingHorizontalOffset = Mathf.Sin(Time.time * floatSpeed * 1.5f) * floatingHorizontalDistance;

        Vector3 newPosition = InitialPosition + Vector3.up * verticalPositionOffset + Vector3.right * floatingHorizontalOffset;
        transform.position = newPosition;

    }
}