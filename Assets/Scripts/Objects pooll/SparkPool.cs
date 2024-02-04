using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkPool : MonoBehaviour
{
    private static SparkPool instance;
    public static SparkPool Instance { get { return instance; } }

    [SerializeField] private GameObject spark;
    private List<GameObject> sparkList;
    private int sparkAmount;

    void Awake()
    {
        instance = this;
        sparkAmount = 5;
        sparkList = new List<GameObject>(sparkAmount);

        for (int i = 0; i < sparkAmount; i++)
        {
            GameObject bulletSpark = Instantiate(spark);
            bulletSpark.transform.SetParent(transform);
            bulletSpark.SetActive(false);
            sparkList.Add(bulletSpark);
        }
    }

        public GameObject GetSpark() 
    {
        foreach (GameObject spark in sparkList)
        {
            //Activate object if is not active in hierarchy
            if(!spark.activeInHierarchy)
            {
                spark.SetActive(true);
                return spark;
            }
        }
        GameObject sparkObject = Instantiate(spark);
        sparkObject.transform.SetParent(transform);
        sparkList.Add(sparkObject);
        return sparkObject;
    }
}
