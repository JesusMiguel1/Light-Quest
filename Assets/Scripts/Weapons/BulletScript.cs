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
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Trigger action...." + other.gameObject.name);
        Destroy(other.gameObject);
    }
}