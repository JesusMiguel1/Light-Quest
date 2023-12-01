using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class DronesPoolManager : MonoBehaviour
    {
        private static DronesPoolManager instance;
        public static DronesPoolManager Instance { get { return instance; } }

        
        GameObject firstDrone;
        GameObject wanderDrone;

        GameObject[] policeDronePrefab;
        private List<GameObject> drones;
        private List<GameObject> wanders;

        private int dronesAmount;
        private int wanderDronesAmount;
        // Start is called before the first frame update
        void Awake()
        {
            dronesAmount = 40;
            wanderDronesAmount = 20;

            instance = this;

            drones = new List<GameObject>(dronesAmount);
            wanders = new List<GameObject>(wanderDronesAmount);

            firstDrone = Resources.Load("slapper", typeof(GameObject)) as GameObject;
            wanderDrone = Resources.Load("R1_Enemy", typeof(GameObject)) as GameObject;

            //policeDronePrefab = new GameObject[] { firstDrone, wanderDrone };
            for (int i = 0; i < dronesAmount; i++)
            {
                //int dronesIndex = i % policeDronePrefab.Length;  // Use modulo to cycle through the prefabs

                GameObject instantiate = Instantiate(firstDrone);
                instantiate.transform.SetParent(transform);
                instantiate.SetActive(false);
                drones.Add(instantiate);
            }

            for (int i = 0;i < dronesAmount; i++) { }
        }

        public GameObject GetDrones()
        {
            foreach (GameObject drone in drones)
            {
                if (!drone.activeInHierarchy)
                {
                    drone.SetActive(true);
                    return drone;
                }
            }
            //int dronesIndex = Random.Range(0, policeDronePrefab.Length);
            GameObject instantiate = Instantiate(firstDrone);
            instantiate.transform.SetParent(transform);
            drones.Add(instantiate);
            return instantiate;
        }

        //public GameObject GetDroneInspector()
        //{
        //    foreach (GameObject drone in drones)
        //    {
        //        if (!drone.activeInHierarchy)
        //        {
        //            drone.SetActive(true);
        //            return drone;
        //        }
        //    }
        //    //int dronesIndex = Random.Range(0, policeDronePrefab.Length);
        //    GameObject instantiate = Instantiate(policeInspector);
        //    instantiate.transform.SetParent(transform);
        //    drones.Add(instantiate);
        //    return instantiate;
        //}

        public GameObject GetWanderDroids()
        {
            foreach (GameObject drone in wanders)
            {
                if (!drone.activeInHierarchy)
                {
                    drone.SetActive(true);
                    return drone;
                }
            }
            //int dronesIndex = Random.Range(0, policeDronePrefab.Length);
            GameObject instantiate = Instantiate(wanderDrone);
            instantiate.transform.SetParent(transform);
            wanders.Add(instantiate);
            return instantiate;
        }
    }
}