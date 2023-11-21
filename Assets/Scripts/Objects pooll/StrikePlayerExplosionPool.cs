using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikePlayerExplosionPool : MonoBehaviour
{
    private static StrikePlayerExplosionPool instance;
    public static StrikePlayerExplosionPool Instance { get { return instance; } }

    private GameObject hitPlayerPrefab;

    private List<GameObject> pieces;
    private int amountOfPieces = 10;

    private void Awake()
    {
        instance = this;
        hitPlayerPrefab = Resources.Load("CenterExplosion", typeof(GameObject)) as GameObject;

        pieces = new List<GameObject>(amountOfPieces);


        for (int i = 0; i < amountOfPieces; i++)
        {

            //We add pieces is there is not pieces
            GameObject instantiate = Instantiate(hitPlayerPrefab);
            instantiate.transform.SetParent(transform);
            instantiate.SetActive(false);
            pieces.Add(instantiate);
        }

    }


    public GameObject GetExplosionPieces()
    {
        foreach (GameObject piece in pieces)
        {
            if (!piece.activeInHierarchy)
            {
                piece.SetActive(true);
                return piece;
            }

        }
        //piecePrefab = Resources.Load("Piece", typeof(GameObject)) as GameObject;
        GameObject instantiate = Instantiate(hitPlayerPrefab);
        instantiate.transform.SetParent(transform);
        pieces.Add(instantiate);

        return instantiate;
    }
}
