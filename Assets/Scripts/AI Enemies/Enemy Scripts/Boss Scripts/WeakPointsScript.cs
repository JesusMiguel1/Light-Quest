using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "Bullet(Clone)")
            //this.gameObject.SetActive(false);
            //Debug.Log($"<b> THIS OBJECT IS COLLIDING WITH... {collision.gameObject.name}</b>");
    }
}
