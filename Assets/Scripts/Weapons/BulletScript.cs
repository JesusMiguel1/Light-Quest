using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 100f;
    float bulletDuration = 1f;
    float bulletLifeTime;

    void OnEnable()
    {
        bulletLifeTime = bulletDuration;
    }

    void Start() { 
}

    // Update is called once per frame
    void Update()
    {
        BulletDirection();
    }

    void BulletDirection()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "R1_Enemy" || collision.gameObject.name == "D1")
        {
            //Destroy(collision.gameObject);
            //Debug.Log("Collision" + collision.gameObject.name);
            collision.gameObject.SetActive(false);
        }
       
    }
    /* private void OnTriggerEnter(Collider other)
     {
         //if(other.gameObject.name == "R1_Enemy" || other.gameObject.name == "Drones")
         //{
             Destroy(other.gameObject);
        // }

     }*/
}