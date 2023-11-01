using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;

    

    public float radius = 10f;
    public float explosionForce = 10f;

    public float delay = 1f;

    Rigidbody rig;

    float bulletSpeed = 3f;
    float bulletDuration = 1f;
    float bulletLifeTime = 0f;

    float range = 15; 



    // Start is called before the first frame update

    

    void Start()
    {

        rig = GetComponent<Rigidbody>();
        
        

    }
    private void OnEnable()
    {
        bulletLifeTime = bulletDuration;
        Invoke("Explode", delay); 

    }


    // Update is called once per frame
    void Update()
    {
        BulletLifeTime();

    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach(Collider near in colliders)
        {
            Rigidbody rigb = near.GetComponent<Rigidbody>();
            
            if(rigb !=null)
            {
                rigb.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse); 
            }
        }
        
        Instantiate(explosionEffect, transform.position, transform.rotation);
        
    }

    void BulletLifeTime()
    {
        //transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        //grenadePrefab = Resources.Load("Grenade", typeof(GameObject)) as GameObject;

        //grenadePrefab.GetComponent<Rigidbody>().AddForce(* range, ForceMode.Impulse);

        bulletLifeTime -= Time.deltaTime;
        if (bulletLifeTime <= 0)
        {
            gameObject.SetActive(false);
            //explosion.SetActive(false);

        }
    }
}
