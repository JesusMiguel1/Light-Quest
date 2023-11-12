using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesPoolManager : MonoBehaviour
{
    private static DronesPoolManager instance;
    public static DronesPoolManager Instance { get { return instance; } }


    GameObject firstDrone;
    GameObject secondDrone;

    GameObject[] policeDronePrefab;
    private List<GameObject> drones;

    private int dronesAmount;
    // Start is called before the first frame update
    void Awake()
    {
        dronesAmount = Random.Range(1, 10);
        instance = this;
        drones = new List<GameObject>(dronesAmount);
        firstDrone = Resources.Load("D1", typeof(GameObject)) as GameObject;
        secondDrone = Resources.Load("R1_Enemy", typeof(GameObject)) as GameObject;

        policeDronePrefab = new GameObject[] { secondDrone, firstDrone };
        int dronesIndex = Random.Range(0, policeDronePrefab.Length);
        for (int i = 0; i < dronesAmount; i++)
        {
            GameObject instantiate = Instantiate(policeDronePrefab[dronesIndex]);
            instantiate.transform.SetParent(transform);
            instantiate.SetActive(false);
            drones.Add(instantiate);
        }
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
        int dronesIndex = Random.Range(0, policeDronePrefab.Length);
        GameObject instantiate = Instantiate(policeDronePrefab[dronesIndex]);
        instantiate.transform.SetParent(transform);
        drones.Add(instantiate);
        return instantiate;
    }
}
