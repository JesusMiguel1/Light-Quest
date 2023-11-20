using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR;
using UnityEngine;

namespace object_pool 
{
    public class ShootingScript : MonoBehaviour
    {
        [SerializeField] private Transform rightHandSpawner;
        [SerializeField] private Transform leftHandSpawner;

        private LayerMask enemyMask;
        private RaycastHit hit;
        private LineRenderer laser;
        private float distance = 100f;
        private float range = 15f;

        public AudioClip audioClip;
        private AudioSource audioSource;

        //public Grenade grenadeScript; 

        void Start()
        {
            enemyMask = 1 << 9;
            laser = GetComponent<LineRenderer>();
            audioSource = GetComponent<AudioSource>();
        }

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

            //var bulletTrace = Instantiate();
            Vector3 forward = transform.TransformDirection(Vector3.forward) * speed;
            if (Physics.Raycast(transform.position, forward, out hit, distance, enemyMask))
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
                if (audioSource != null && audioClip != null)
                {
                    audioSource.volume = 0.05f;
                    audioSource.PlayOneShot(audioClip);
                }
            }
            if (leftHandInput.GetButtonDown(VRButton.Trigger))
            {
                //Debug.Log("Lets start shooting");
                LeftHandShood();
                if (audioSource != null && audioClip != null)
                {
                    audioSource.volume = 0.05f;
                    audioSource.PlayOneShot(audioClip);
                }
            }

        }
        private void RightHandShood()
        {
            if (Physics.Raycast(rightHandSpawner.position, rightHandSpawner.forward, out hit))
            {
                GameObject bullet = BulletsPoolManager.Instance.GetBullet();
                bullet.transform.position = rightHandSpawner.position;
                bullet.transform.rotation = rightHandSpawner.rotation;

                //GameObject grenade = BulletsPoolManager.Instance.GetGrenade();
                //grenade.transform.position = rightHandSpawner.position;
                //grenade.transform.rotation = rightHandSpawner.rotation;

                //grenade.GetComponent<Rigidbody>().AddForce( rightHandSpawner.forward * range, ForceMode.Impulse);


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
}

