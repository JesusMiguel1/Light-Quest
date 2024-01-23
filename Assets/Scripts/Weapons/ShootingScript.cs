using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR.Input;
using Liminal.SDK.VR;
using UnityEngine;
using object_pool;

namespace object_pool
{
    public class ShootingScript : MonoBehaviour
    {

        LeftAndRightHand hands;
        //private LayerMask enemyMask;
        //private float distance = 100f;
        //private float range = 15f;

        void Start()
        {
            //enemyMask = 1 << 9;
            hands = GetComponent<LeftAndRightHand>();
        }

        void Update()
        {
            //CheckHand();
            Fire();
        }

        #region GETTING IPUTS FOR THE HANDS
        //void CheckHand()
        //{
        //    var rightHandInput = GetInput(VRInputDeviceHand.Right);
        //    var leftHandInput = GetInput(VRInputDeviceHand.Left);

        //    if (rightHandInput.GetAxis1D(VRAxis.Three) > 0)
        //    {
        //        //The hands will be used here 
        //    }
        //}
        #endregion



        #region GETTING DEVICE INPUT 
        private IVRInputDevice GetInput(VRInputDeviceHand hand)
        {
            var device = VRDevice.Device;
            //Debug.Log("Checking hands..." + hand);
            return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
        }
        #endregion




        #region GUN FIRE METHOD
        private void Fire()
        {
            var rightHandInput = GetInput(VRInputDeviceHand.Right);
            var leftHandInput = GetInput(VRInputDeviceHand.Left);

            if (rightHandInput.GetButtonDown(VRButton.One)) //One will be replaced for Trigger
            {
                //WE GONNA THE PULL TRIGGER ANIMATION HERE 

                //Debug.Log("Lets start shooting");
                hands.RightHandShood();
                hands.audioSource.Play();
                //if (audioSource != null && audioClip != null)
                //{
                //    audioSource.volume = 0.05f;
                //    audioSource.PlayOneShot(audioClip);
                //}
                //FindObjectOfType<AudioIntroManager>().PlaySound("Shoot");
            }

            /******THIS NEEDS TO BE MOVED TO THE HANDS OWN SCRIPT******/

            //if (rightHandInput.GetAxis1D(VRAxis.Three) > 0)
            //{
            //    //We gonna the hands grab animations here 
            //    Debug.Log($"<b> GRABBING RIGHT HAND GUN </b>");
            //}



            /******WE CAN USE THAT AS POWER UP FOR GUNMACHINE SHOOTING*******/

            //if (rightHandInput.GetAxis1D(VRAxis.Two) > 0 && powerUp == true)
            //{
            //    //We gonna the hands grab animations here 
            //    RightHandShood();
            //}

            //SAME FOR THE LEFT HAND SHOOTING



            // LEFT HAND SHOOTING TRIGGER
            if (leftHandInput.GetButtonDown(VRButton.Trigger))
            {
                //WE GONNA THE PULL TRIGGER ANIMATION HERE 

                //Debug.Log("Lets start shooting");
                hands.LeftHandShood();
                hands.audioSource.Play();
                //if (audioSource != null && audioClip != null)
                //{
                //    audioSource.volume = 0.05f;
                //    audioSource.PlayOneShot(audioClip);
                //FindObjectOfType<AudioIntroManager>().PlaySound("Shoot");
                //}
            }

            //if (leftHandInput.GetAxis1D(VRAxis.Three) > 0)
            //{
            //    //We gonna the hands grab animations here 
            //    Debug.Log($"<b> GRABBING LEFT HAND GUN </b>");
            //}
        }
        #endregion

    }
}