using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 100f;
    float bulletDuration = 2f;
    float bulletLifeTime;

    GameObject explosion;
    //public SlowMotion slow;
    //ParticlesExplosion explosion;

    //public SlowMotion powerup; 
    void OnEnable()
    {
        bulletLifeTime = bulletDuration;
        //explosion = GetComponent<ParticlesExplosion>();

    }


    // Update is called once per frame
    void Update()
    {
        BulletLifeTime();
        //slow.SlowMotionPower();
    }

    void BulletLifeTime()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            gameObject.SetActive(false);
            explosion.SetActive(false);

        }
    }


    void OnCollisionEnter(Collision other)

    {
        if(other.gameObject.name == "D1" || other.gameObject.name == "R1_Enemy" || other.gameObject.name == "Powerup" )
        {
            //Destroy(collision.gameObject);
            Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>" );
            other.gameObject.SetActive(false);
            //explosion.OnDestroyObject();
            ColorfullExplosion();
           
        }


      void ColorfullExplosion()
        {
            explosion = DestructiblePoolManager.Instance.GetPieces();
            explosion.transform.position = transform.position;
           
        }
    }
}