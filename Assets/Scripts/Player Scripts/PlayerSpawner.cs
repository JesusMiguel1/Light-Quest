using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject;
        player.transform.position = new Vector3(0f, 3.2f, 0f);
    }

   
}
