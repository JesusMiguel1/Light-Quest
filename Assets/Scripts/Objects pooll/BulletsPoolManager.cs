using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class BulletsPoolManager : MonoBehaviour
    {
        private static BulletsPoolManager instance;
        public static BulletsPoolManager Instance { get { return instance; } }

        GameObject bulletPrefab;
        [SerializeField] List<GameObject> bullets;
        [SerializeField] int bulletsAmount = 10;

        GameObject grenadePrefab;
        [SerializeField] List<GameObject> grenades;
        [SerializeField] int grenadesAmount = 10;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;

            //I needs to get the bullet prefab from the resources folder
            grenadePrefab = Resources.Load("Grenade", typeof(GameObject)) as GameObject;

            bulletPrefab = Resources.Load("Bullet", typeof(GameObject)) as GameObject;

            bullets = new List<GameObject>(bulletsAmount);

            for (int i = 0; i < bulletsAmount; i++)
            {
                GameObject instantiate = Instantiate(bulletPrefab);
                instantiate.transform.SetParent(transform);
                instantiate.SetActive(false);
                bullets.Add(instantiate);
            }

            //grenades = new List<GameObject>(grenadesAmount);

            //for (int i = 0; i < grenadesAmount; i++)
            //{
            //    GameObject instantiate = Instantiate(grenadePrefab);
            //    instantiate.transform.SetParent(transform);
            //    instantiate.SetActive(false);
            //    grenades.Add(instantiate);
            //}

        }

        public GameObject GetBullet()
        {
            foreach (GameObject bullet in bullets)
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }
            GameObject instantiate = Instantiate(bulletPrefab);
            instantiate.transform.SetParent(transform);
            bullets.Add(instantiate);
            return instantiate;
        }
        public GameObject GetGrenade()
        {
            foreach (GameObject grenade in grenades)
            {
                if (!grenade.activeInHierarchy)
                {
                    grenade.SetActive(true);
                    return grenade;
                }
            }
            GameObject instantiate = Instantiate(grenadePrefab);
            instantiate.transform.SetParent(transform);
            grenades.Add(instantiate);
            return instantiate;
        }
    }

}