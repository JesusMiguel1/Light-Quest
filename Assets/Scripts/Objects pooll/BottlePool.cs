using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePool : MonoBehaviour
{
    private GameObject bottle;
    private List<GameObject> bottlelist;
    private GlobalStrings strings;

    private int amountOfBottles;


    void Awake()
    {
        amountOfBottles = 10;
        strings = new GlobalStrings();
        bottlelist = new List<GameObject>();
        bottle = Resources.Load(strings.Bottle, typeof(GameObject)) as GameObject;
       
        bottle.transform.localScale = new Vector3(15f,15f,15f);


        for (int i = 0; i < amountOfBottles; i++)
        {
            GameObject instantiate = Instantiate(bottle);
            //instantiate.transform.position = new Vector3(5f, -5.2236f, 10f);
            instantiate.transform.position = new Vector3(Random.Range(-20, 20), -5.2236f, Random.Range(-10, 40));
            instantiate.transform.SetParent(transform);
            bottlelist.Add(instantiate);
        }
    }
}
