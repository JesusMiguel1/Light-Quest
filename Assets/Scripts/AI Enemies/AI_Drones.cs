using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace punk_vs_robots
{
    public class AI_Drones : MonoBehaviour
    {
        private float moveSpeed = 5f;
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


        //Vector3 GetRandomDirection()
        //{
        //    // Get a random direction for patrolling
        //    float randomX = Random.Range(-1f, 20f);
        //    float randomZ = Random.Range(-1f, 20f);
        //    float randomY = Random.Range(-1f, 2f);

        //    return new Vector3(randomX,  randomZ).normalized;
        //}



        ////Way point to create AI nav path

        //private float moveSpeed = 5f;
        //public float rotationSpeed = 3.0f;
        //public float detectPlayerRadius = 5.0f;
        //public float wayPointDistance = 0;
        //Transform player; // Reference to the player's transform
        //Vector3 movement;

        //Rigidbody rb;


        //void Start()
        //{
        //    //InitialPosition = transform.position;
        //    rb = this.GetComponent<Rigidbody>();

        //    if (player == null)
        //    {
        //        player = GameObject.FindGameObjectWithTag("Player").transform;
        //    }
        //}

        //void Update()
        //{
        //    Vector3 moveDirection = player.position - transform.position;
        //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //    Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //    rb.rotation = rotation;
        //    moveDirection.Normalize();
        //    movement = moveDirection;

        //    transform.LookAt(player.transform.position);
        //}
        //private void FixedUpdate()
        //{
        //    MoveDroneTowardsPlayer(movement);
        //}

        //void MoveDroneTowardsPlayer(Vector3 direction) {

        //    rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        //}

    }
}