using UnityEngine;

public class IgnoreDroneCollisions : MonoBehaviour
{
    public GameObject dronePrefab;
    public GameObject nextDronePrefab;
    public GameObject[] prefabsArray;
    void Start()
    {
        prefabsArray = new GameObject[] {dronePrefab, nextDronePrefab};
        // Get all colliders attached to this GameObject
        Collider[] myColliders = GetComponentsInChildren<Collider>();

        // Loop through all GameObjects with the "Drone" tag
        foreach (GameObject drone in prefabsArray)
        {
            // Check if the GameObject is not the current drone
            if (drone != gameObject)
            {
                // Get all colliders attached to the other drone
                Collider[] otherColliders = drone.GetComponentsInChildren<Collider>();

                // Ignore collisions between colliders of this drone and the other drone
                foreach (Collider myCollider in myColliders)
                {
                    foreach (Collider otherCollider in otherColliders)
                    {
                        Physics.IgnoreCollision(myCollider, otherCollider);
                    }
                }
            }
        }
    }
}

