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
        private float distance = 100f;
        private float range = 15f;
        public TrailRenderer bulletTrace;
        public AudioClip audioClip;
        private AudioSource audioSource;
        public AudioClip hitAudioClip;
        //public AudioClip policeAudioClips;
        private AudioManager audioManager;

        private HashSet<string> allowedNames;
        GlobalStrings strings;
        GlobalBool gbool;
        //public Grenade grenadeScript; 

        GameObject explosion;

        void OnEnable()
        {
            audioSource = GetComponent<AudioSource>();
            audioManager = GetComponent<AudioManager>();
            
        }
        void Start()
        {
            enemyMask = 1 << 9;
            

            strings = new GlobalStrings();
            gbool = new GlobalBool();

            allowedNames = new HashSet<string> { 
                strings.slapperClone, 
                "R1_Enemy(Clone)", 
                "Powerup", 
                strings.EnemyTrigger,
                strings.policeInspectorClone
            };
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
                audioSource.Play(); 
                //if (audioSource != null && audioClip != null)
                //{
                //    audioSource.volume = 0.05f;
                //    audioSource.PlayOneShot(audioClip);
                //}
                //FindObjectOfType<AudioIntroManager>().PlaySound("Shoot");
            }
            //if (leftHandInput.GetButtonDown(VRButton.Trigger))
            //{
            //    //Debug.Log("Lets start shooting");
            //    LeftHandShood();
            //    audioSource.Play();
            //    //if (audioSource != null && audioClip != null)
            //    //{
            //    //    audioSource.volume = 0.05f;
            //    //    audioSource.PlayOneShot(audioClip);
            //    //FindObjectOfType<AudioIntroManager>().PlaySound("Shoot");
            //    //}
            //}



        }
        private void RightHandShood()
        {
            var trace = Instantiate(bulletTrace, rightHandSpawner.position, Quaternion.identity);
            trace.AddPosition(rightHandSpawner.position);   

            if (Physics.Raycast(rightHandSpawner.position, rightHandSpawner.forward, out hit))
            {
                GameObject bullet = BulletsPoolManager.Instance.GetBullet();
                bullet.transform.position = rightHandSpawner.position;
                bullet.transform.rotation = rightHandSpawner.rotation;
                Debug.DrawRay(rightHandSpawner.position, rightHandSpawner.forward * hit.distance, Color.green) ;
                //Debug.Log($"<b> WHAT DID I HIT </b> ....{hit.collider.name}");
                trace.transform.position = hit.point;

                //if(hit.collider.name == "EnemyTrigger")
                //{
                //    Debug.Log($"<b> DO SOMETHINGT </b> ....{hit.collider.name}");
                //    hit.collider.gameObject.SetActive(false);
                //}
                if (hit.collider.gameObject.name == strings.slapperClone)
                {
                    AudioManager.Instance.PlayOneShot(hitAudioClip);

                    audioManager = GetComponent<AudioManager>();
                    if (audioManager != null)
                    {
                        audioManager.PlayOneShot(hitAudioClip);
                    }
                    //FindObjectOfType<MainAudioManager>().PlaySound("Shoot");
                }

                if (allowedNames.Contains(hit.collider.name))
                {
                   
                    // int clipsIndex = Random.Range(0, audioClips.Length);

                    //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                    hit.collider.gameObject.SetActive(false);
                    gbool.inspectorDisabled = true;
                    ColorfullExplosion();

                }
               

                //GameObject grenade = BulletsPoolManager.Instance.GetGrenade();
                //grenade.transform.position = rightHandSpawner.position;
                //grenade.transform.rotation = rightHandSpawner.rotation;

                //grenade.GetComponent<Rigidbody>().AddForce( rightHandSpawner.forward * range, ForceMode.Impulse);


            }
        }

        private void LeftHandShood()
        {
            var trace = Instantiate(bulletTrace, leftHandSpawner.position, Quaternion.identity);
            trace.AddPosition(leftHandSpawner.position);
            if (Physics.Raycast(leftHandSpawner.position, leftHandSpawner.forward, out hit))
            {
                GameObject bullet = BulletsPoolManager.Instance.GetBullet();
                bullet.transform.position = leftHandSpawner.position;
                bullet.transform.rotation = leftHandSpawner.rotation;
                trace.transform.position = hit.point;
                if (hit.collider.gameObject.name == strings.slapperClone)
                {
                    AudioManager.Instance.PlayOneShot(hitAudioClip);

                    //audioManager = GetComponent<AudioManager>();
                    if (audioManager != null)
                    {
                        audioManager.PlayOneShot(hitAudioClip);
                    }
                }
                if (allowedNames.Contains(hit.collider.name))
                {
                    //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                    hit.collider.gameObject.SetActive(false);
                    ColorfullExplosion();

                }
            }
        }

        void ColorfullExplosion()
        {
            explosion = DestructiblePoolManager.Instance.GetPieces();
            explosion.transform.position = hit.point;

        }
    }
}

