using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] Transform bulletSpawner;
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
            Debug.Log("Lets start shooting");
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                GameObject bullet = BulletsPoolManager.Instance.GetBullet();
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
            }
        }

    }
}
