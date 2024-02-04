using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointSpark : MonoBehaviour
{
    private static HitPointSpark instance;
    public static HitPointSpark Instance { get { return instance; } }

    [SerializeField] private GameObject hitSpark;
    private List<GameObject> hitSparklList;
    private int sparkCount;

    void Awake()
    {
        instance = this;
        sparkCount = 5;
        hitSparklList = new List<GameObject>(sparkCount);

        for (int i = 0; i < sparkCount; i++)
        {
            GameObject spark = Instantiate(hitSpark);
            spark.transform.SetParent(transform);
            spark.SetActive(false);
            hitSparklList.Add(spark);
        }

    }

    public GameObject GetHitSpark()
    {
        foreach (GameObject hit in hitSparklList)
        {
            if (!hit.activeInHierarchy)
            {
                hit.SetActive(true);
                return hit;
            }
        }
        GameObject instance = Instantiate(hitSpark);
        instance.transform.SetParent(transform);
        hitSparklList.Add(instance);
        return instance;
    }
}
