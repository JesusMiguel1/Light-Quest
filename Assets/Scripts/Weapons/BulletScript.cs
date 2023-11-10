using Liminal.SDK.VR.Avatars.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 100f;
    float bulletDuration = 1f;
    float bulletLifeTime;
    
    GameObject explosion;
    
    private HashSet<string> allowedNames;

    void OnEnable()
    {
        allowedNames = new HashSet<string> { "D1(Clone)", "R1_Enemy(Clone)", "Powerup", "Sphere" };
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
           // explosion.SetActive(false);

        }
    }


    void OnCollisionEnter(Collision other)

    {
        if (allowedNames.Contains(other.gameObject.name))
        {
            //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
            other.gameObject.SetActive(false);
            ColorfullExplosion();
            
        }
       


        //if(other.gameObject.name == "D1(Clone)" || other.gameObject.name == "R1_Enemy" || other.gameObject.name == "Powerup" || other.gameObject.name == "Sphere")
        //{
        //    //Destroy(collision.gameObject);
        //    Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>" );
        //    other.gameObject.SetActive(false);
        //    //explosion.OnDestroyObject();
        //    ColorfullExplosion();
        //}


        void ColorfullExplosion()
        {
            explosion = DestructiblePoolManager.Instance.GetPieces();
            explosion.transform.position = transform.position;
           
        }
    }
}