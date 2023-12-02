using System.Collections;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

namespace object_pool
{
    public class AI_Drones : MonoBehaviour
    {
        //Test
        [Header("way points for enemy to patrol")]
        [Tooltip("Drop the waypoints game objects into the editor fields")]
        public Transform pointA;
        public Transform pointB;
        public Transform pointC;
        private Transform target;

        [Header("Siren flash light")]
        [Tooltip("Drop the light point game object located into slapper game object upper_r file")]
        public GameObject siren;


        private GlobalSpeedManager speed;

        public float moveSpeed;
        public bool ifMovingToPlayer;
        public Transform player; // Reference to the player's transform
        Rigidbody rb;

        GameObject wayPointPrefab;
        GameObject explosion;

        public AudioClip audioClips;
        public AudioClip policeAudioClips;
        AudioSource audioSource;

        private AudioManager audioManager;

        void OnEnable()
        {
            StartCoroutine(FlashingLightSiren());
            audioSource = GetComponent<AudioSource>();

            audioManager = GetComponent<AudioManager>();
            if (audioSource != null && policeAudioClips != null)
            {
                audioSource.volume = 1f;
                audioSource.PlayOneShot(policeAudioClips);
            }
            speed = new GlobalSpeedManager();
        }
        void Start()
        {
            
            ifMovingToPlayer = false;
            //player = Resources.Load()
            //rb = this.GetComponent<Rigidbody>();
            SetNewTarget();
        }

        void Update()
        {
            if (!ifMovingToPlayer)
            {
                PatrolMovement();
            }
            
            StartCoroutine(StartMoveToPlayer());

            if (ifMovingToPlayer)
            {
                MoveToPlayer();
                speed.CurrentSpeed = 30f;
            }
        }

        void PatrolMovement()
        {
            //moveSpeed = speed.CurrentSpeed;
            ifMovingToPlayer = false;
            pointA.position = new Vector3(0,0, 50f);
            pointB.position = new Vector3(40, 3, -40f);
            pointC.position = new Vector3(-40, 0, 0f);

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed.CurrentSpeed * Time.deltaTime);

            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = rotation;


            // Check if the object reaches the target position
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                SetNewTarget();
            }
        }

        void SetNewTarget()
        {
            // Randomly select the next target point (A, B, or C)
            int randomIndex = Random.Range(0, 3);
            if (randomIndex == 0)
                target = pointA;
            else if (randomIndex == 1)
                target = pointB;
            else
                target = pointC;
        }

        IEnumerator StartMoveToPlayer()
        {
            float timing = 1f;
            yield return new WaitForSeconds(timing);
            ifMovingToPlayer = true;
            moveSpeed = speed.CurrentSpeed;
        }

        void MoveToPlayer()
        {
            moveSpeed = speed.CurrentSpeed;
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = rotation;
            transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
           
        }

        void OnCollisionEnter(Collision other){
            if (other.gameObject.name == "Player(Clone)")
            {
                Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                ifMovingToPlayer = false;
                gameObject.SetActive(false);
                ColorfullExplosion();

            }
            if (other.gameObject.name == "BulletTrace(Clone)")
            {
                AudioManager.Instance.PlayOneShot(audioClips);
                // int clipsIndex = Random.Range(0, audioClips.Length);
                if (audioManager != null)
                {
                    audioManager.PlayOneShot(audioClips);
                }
            }
        }

        void ColorfullExplosion()
        {
            explosion = StrikePlayerExplosionPool.Instance.GetExplosionPieces();
            explosion.transform.position = transform.position;
        }

        IEnumerator FlashingLightSiren()
        {
            float flashingTime = 0.2f;
            yield return new WaitForSeconds(flashingTime);
            siren.SetActive(true);

            yield return new WaitForSeconds(flashingTime);
            siren.SetActive(false);
            StartCoroutine(FlashingLightSiren());
        }
    }
}