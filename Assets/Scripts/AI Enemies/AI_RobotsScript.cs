using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace punk_vs_robots
{
    public class AI_RobotsScript : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private float rotationSpeed = 20;
        private float detectPlayerRadius = 0.1f;
        public bool ifMovingToPlayer = false;
        Vector3 movement;
        [SerializeField] Transform player; // Reference to the player's transform
        Rigidbody rb;

        GameObject wayPointPrefab;
        GameObject explosion;

        public AudioClip audioClips;
        public AudioClip policeAudioClips;
        AudioSource audioSource;

        private AudioManager audioManager;


        void Start()
        {

            rb = this.GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();

            //if (player == null)
            //{
            //    //player = GameObject.FindGameObjectWithTag("Player").transform;
            //}
            audioManager = GetComponent<AudioManager>();

            // Initialize patrol direction
        }

        void Update()
        {

            if (audioSource != null && policeAudioClips != null)
            {
                audioSource.volume = 0.05f;
                audioSource.PlayOneShot(policeAudioClips);
            }
            //if (!ifMovingToPlayer)
            //{
            //    float randomX = Random.Range(0f, 20f);
            //    float randomY = Random.Range(0f, 5f);
            //    float randomZ = Random.Range(20f, 0f);

            //    Vector3 newPosition = new Vector3(randomX, randomY, randomZ);
            //    transform.Translate(newPosition * moveSpeed * Time.deltaTime);
            //}


            StartCoroutine(MoveToPlayer());


        }


        #region Move towards player 
        IEnumerator MoveToPlayer()
        {
            float randomTime = Random.Range(0.1f, 4.1f);
            yield return new WaitForSeconds(randomTime);
            ifMovingToPlayer = true;
            Vector3 moveDirection = player.position - transform.position;
            if (moveDirection.magnitude >= detectPlayerRadius && ifMovingToPlayer)
            {
                moveSpeed = Random.Range(10f, 25f);
                // Rotate towards the player
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Move towards the player
                movement = moveDirection.normalized;
            }

        }
        #endregion


        #region Fixed update move the drone
        private void FixedUpdate()
        {
            MoveDroneTowardsPlayer(movement);
        }

        void MoveDroneTowardsPlayer(Vector3 direction)
        {
            // Move the drone using rigidbody
            rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        }
        #endregion

        void OnCollisionEnter(Collision other)

        {
            if (other.gameObject.name == "Player")
            {
                //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                gameObject.SetActive(false);
                ColorfullExplosion();

            }
            if (other.gameObject.name == "Bullet(Clone)")
            {
                AudioManager.Instance.PlayOneShot(audioClips);
                //// int clipsIndex = Random.Range(0, audioClips.Length);
                //if (audioManager != null)
                //{
                //    audioManager.PlayOneShot(audioClips);
                //}
            }
        }

        void ColorfullExplosion()
        {
            explosion = DestructiblePoolManager.Instance.GetPieces();
            explosion.transform.position = transform.position;

        }
    }
    }
