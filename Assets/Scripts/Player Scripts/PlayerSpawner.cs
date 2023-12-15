using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject; 
    }

   
}
