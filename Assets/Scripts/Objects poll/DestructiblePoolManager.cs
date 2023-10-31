using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePoolManager : MonoBehaviour
{
    private static DestructiblePoolManager instance;
    public static DestructiblePoolManager Instance { get { return instance; } }

    private GameObject piecePrefab;

    private List<GameObject> pieces;
    private int amountOfPieces = 10;

    private void Awake()
    {
        instance = this;
         piecePrefab = Resources.Load("ParticlesExplosion", typeof(GameObject)) as GameObject;
        
        pieces = new List<GameObject>(amountOfPieces);
       

        for (int i = 0; i < amountOfPieces; i++)
        {
           
            //We add pieces is there is not pieces
            GameObject instantiate = Instantiate(piecePrefab);
            instantiate.transform.SetParent(transform);
            instantiate.SetActive(false);
            pieces.Add(instantiate);
        }
    }

    //Getting the pieces
    public GameObject GetPieces() 
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
        GameObject instantiate = Instantiate(piecePrefab);
        instantiate.transform.SetParent(transform);
        pieces.Add(instantiate);

        return instantiate;
    }
}
