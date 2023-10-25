using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 100f;
    float bulletDuration = 1f;
    float bulletLifeTime;
    public SlowMotion slow;
    public GameObject cube;

    public SlowMotion powerup; 
    void OnEnable()
    {
        bulletLifeTime = bulletDuration;
    }

    void Start() 
    {
       // cube = Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject;
    
    }

    // Update is called once per frame
    void Update()
    {
        BulletDirection();
        slow.SlowMotionPower();

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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "R1_Enemy" || other.gameObject.name == "Powerup")
        {
            //Destroy(collision.gameObject);
            //Debug.Log("Collision" + collision.gameObject.name);
            other.gameObject.SetActive(false);
            

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