using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    Vector3 rotation;
    float speed;

    [SerializeField] float objectSize = 0.1f;
    public float explosionForce = 10f;
    public float explosionRadius = 8f;
    public float explosionUp = 0.4f;
    int rowOfObjects = 4;

    public float delayDust;
    private float pivotObjectDistance;
    private Vector3 objectPivot;
    void Start()
    {
        speed = 5f;
        rotation.y = 30f;

        pivotObjectDistance = objectSize * rowOfObjects / 8;
        objectPivot = new Vector3(pivotObjectDistance, pivotObjectDistance, pivotObjectDistance); //Distance betwwen objects
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 yRotation = new Vector3(0, rotation.y, 0);

    }

    public void OnDestroyObject()
    {
        gameObject.SetActive(false);
        for (int x=0; x < rowOfObjects; x++)
        {
            for (int y=0; y < rowOfObjects; y++)
            {
                for(int z=0; z < rowOfObjects; z++)
                {
                    ConvertingObjectToPieces(x,y,z);
                }
            }
        }

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUp);
            }
        }
    }

    private void ConvertingObjectToPieces(int x, int y, int z)
    {
        GameObject pieces = DestructiblePoolManager.Instance.GetPieces();
        pieces.transform.position = transform.position + new Vector3(objectSize * x, objectSize * y, objectSize * z) - objectPivot;
        pieces.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
        pieces.GetComponent<Rigidbody>().mass = objectSize;
    }
}
