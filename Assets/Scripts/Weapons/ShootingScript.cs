using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] Transform rightHandSpawner;
    [SerializeField] Transform leftHandSpawner;
    RaycastHit hit;
    float distance = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemy();
        Fire();

    }

    //Raycast to find enemy to shoot at them
    void CheckIfEnemy()
    {
        //Ray ray;
        float speed = 10f;


        Vector3 forward = transform.TransformDirection(Vector3.forward) * speed;
        if (Physics.Raycast(transform.position, forward, out hit, distance, LayerMask.GetMask("Enemy")))
        {
            Debug.DrawRay(transform.position, forward, Color.green);
            //Debug.Log("Hitting something... " + hit.point);
        }
    }

    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        
        //Debug.Log("Checking hands..." + hand);
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;

    }

    private void Fire()
    {
        var rightHandInput = GetInput(VRInputDeviceHand.Right);
        var leftHandInput = GetInput(VRInputDeviceHand.Left);

        if (rightHandInput.GetButtonDown(VRButton.One))
        {
            //Debug.Log("Lets start shooting");
            RightHandShood();
            
        }
        if (leftHandInput.GetButtonDown(VRButton.Trigger))
        {
            //Debug.Log("Lets start shooting");
            LeftHandShood();

        }

    }
    private void RightHandShood()
    {
        if (Physics.Raycast(rightHandSpawner.position, rightHandSpawner.forward, out hit))
        {
            GameObject bullet = BulletsPoolManager.Instance.GetBullet();
            bullet.transform.position = rightHandSpawner.position;
            bullet.transform.rotation = rightHandSpawner.rotation;
        }
    }

    private void LeftHandShood()
    {
        if (Physics.Raycast(leftHandSpawner.position, leftHandSpawner.forward, out hit))
        {
            GameObject bullet = BulletsPoolManager.Instance.GetBullet();
            bullet.transform.position = leftHandSpawner.position;
            bullet.transform.rotation = leftHandSpawner.rotation;
        }
    }
}
