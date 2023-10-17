using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesPoolManager : MonoBehaviour
{
    //Singleton
    private static PiecesPoolManager instance;
    public static PiecesPoolManager Instance { get { return instance; } }

    List<GameObject> pieces;
    GameObject piecePrefab;
    int amountOfPieces = 20;

    // Start is called before the first frame update
    void Start()
    {
        //I needs to get the piece prefab here  
        instance = this;
        piecePrefab = Instantiate(Resources.Load("Piece", typeof(GameObject))) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
