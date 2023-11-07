using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDrone : MonoBehaviour
{
    public bool droneDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        //enemies = GetComponent<ActivateEnemies>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Bullet")
        {
            //spawner.DroneDestroyed = true;
            //enemies.isFirstDroneDestroyed = true;
            droneDestroyed = true;
            Debug.Log($"<b>Collision With bullet </b> <color=blue> <b>{other.gameObject.name}</b> </color>");
        }
    }
}
