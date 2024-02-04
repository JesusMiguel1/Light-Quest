using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class BulletScript : MonoBehaviour
    {
        float bulletSpeed = 1000f;
        float bulletDuration = 1f;
        float bulletLifeTime;


        void OnEnable()
        {
            bulletLifeTime = bulletDuration;
        }

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

            }
        }
    }
}