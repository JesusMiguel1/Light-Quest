using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class BulletScript : MonoBehaviour
    {
        float bulletSpeed = 180f;
        float bulletDuration = 1f;
        float bulletLifeTime;

       // GameObject explosion;
        

        private HashSet<string> allowedNames;
        GlobalStrings strings;

        void OnEnable()
        {
            //strings = new GlobalStrings();
            //allowedNames = new HashSet<string> { strings.slapperClone, "R1_Enemy(Clone)", "Powerup",strings.EnemyTrigger };
            bulletLifeTime = bulletDuration;

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

        //void OnCollisionEnter(Collision other)

        //{
        //    if (allowedNames.Contains(other.gameObject.name))
        //    {
        //        //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
        //        other.gameObject.SetActive(false);
        //        ColorfullExplosion();

        //    }



            //if(other.gameObject.name == "D1(Clone)" || other.gameObject.name == "R1_Enemy" || other.gameObject.name == "Powerup" || other.gameObject.name == "Sphere")
            //{
            //    //Destroy(collision.gameObject);
            //    Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>" );
            //    other.gameObject.SetActive(false);
            //    //explosion.OnDestroyObject();
            //    ColorfullExplosion();
            //}


            //void ColorfullExplosion()
            //{
            //    explosion = DestructiblePoolManager.Instance.GetPieces();
            //    explosion.transform.position = transform.position;

            //}
        //}
    }
}