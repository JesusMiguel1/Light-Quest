using punk_vs_robots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredBottlePoolManager : MonoBehaviour
{
    private static ShatteredBottlePoolManager instance;
    public static ShatteredBottlePoolManager Instance { get { return instance; } }

    private GlobalStrings strings;
    private GameObject shatteredBottlePref;
    private List<GameObject> shatteredBottles;
    private int bottleAmount = 10;

   void Awake()
    {
        instance = this;
        strings = new GlobalStrings();
        shatteredBottles = new List<GameObject>(bottleAmount);

        shatteredBottlePref = Resources.Load(strings.ShatteredBottle, typeof(GameObject)) as GameObject;

        for (int i = 0; i < bottleAmount; i++)
        {
            GameObject instantiate = Instantiate(shatteredBottlePref);
            instantiate.transform.SetParent(transform);
            instantiate.SetActive(false);
            shatteredBottles.Add(instantiate);
        }
    }

    public GameObject GetGlasses()
    {
        foreach (GameObject pieces in shatteredBottles)
        {
            if (!pieces.activeInHierarchy)
            {
                pieces.SetActive(true);
                return pieces;
            }
        }
        GameObject instantiate = Instantiate(shatteredBottlePref);
        instantiate.transform.SetParent(transform);
        instantiate.SetActive(false);

        return instantiate;
    }
}
