using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct MoveToPlayerJob : IJobParallelForTransform
{
    public float deltaTime;
    public float detectPlayerRadius;
    public float rotationSpeed;
    public float moveSpeed;
    public float rotationSmoothing;
    public float movementSmoothing;
    public Vector3 playerPosition;

    public void Execute(int index, TransformAccess transform)
    {
        Vector3 moveDirection = playerPosition - transform.position;

        if (moveDirection.magnitude >= detectPlayerRadius)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);

            float currentMoveSpeed = moveSpeed;
            Vector3 movement = moveDirection.normalized;
            transform.position += movement * currentMoveSpeed * deltaTime;

            // Debug logs for debugging
            Debug.Log($"MoveDirection: {moveDirection}, TargetRotation: {targetRotation.eulerAngles}, CurrentPosition: {transform.position}");
        }
    }
}

//public struct MoveToPlayerJob : IJobParallelForTransform
//{
//    public float deltaTime;
//    public float detectPlayerRadius;
//    public float rotationSpeed;
//    public float moveSpeed; // Add moveSpeed parameter to the job
//    public Vector3 playerPosition;

//    public void Execute(int index, TransformAccess transform)
//    {
//        Vector3 moveDirection = playerPosition - transform.position;

//        if (moveDirection.magnitude >= detectPlayerRadius)
//        {
//            // Use the moveSpeed parameter in the calculation
//            float currentMoveSpeed = moveSpeed;

//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);

//            Vector3 movement = moveDirection.normalized;
//            transform.position += movement * currentMoveSpeed * deltaTime;
//        }
//    }
//}

//public struct MoveToPlayerJob : IJobParallelForTransform
//{

//    public float deltaTime;
//    public float detectPlayerRadius;
//    public float rotationSpeed;
//    public float randomTime;

//    [ReadOnly]
//    public Vector3 playerPosition;

//    public void Execute(int index, TransformAccess transform)
//    {
//        // Use the provided randomTime instead of generating a new random value
//        // ...
//        Vector3 moveDirection = playerPosition - transform.position;
//        if (moveDirection.magnitude >= detectPlayerRadius)
//        {
//            float moveSpeed = Random.Range(10f, 25f);

//            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);

//            Vector3 movement = moveDirection.normalized;
//            transform.position += movement * moveSpeed * deltaTime;
//        }
//    }
//public float deltaTime;
//public float detectPlayerRadius;
//public float rotationSpeed;

//[ReadOnly]
//public Vector3 playerPosition;

//public void Execute(int index, TransformAccess transform)
//{
//    Vector3 moveDirection = playerPosition - transform.position;

//    if (moveDirection.magnitude >= detectPlayerRadius)
//    {
//        float moveSpeed = UnityEngine.Random.Range(10f, 25f);

//        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * deltaTime);

//        Vector3 movement = moveDirection.normalized;
//        transform.position += movement * moveSpeed * deltaTime;
//    }
//}
//}

