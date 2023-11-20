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
        public Transform pointA;
        public Transform pointB;
        public Transform pointC;
        private Transform target;

        public EnemyManager enemyManager;






        public float moveSpeed;
        private float rotationSpeed = 20;
        private float detectPlayerRadius = 0.1f;
        public bool ifMovingToPlayer;
        public Transform player; // Reference to the player's transform
        Rigidbody rb;

        GameObject wayPointPrefab;
        GameObject explosion;

        public AudioClip audioClips;
        public AudioClip policeAudioClips;
        AudioSource audioSource;

        private AudioManager audioManager;


        void Start()
        {
            ifMovingToPlayer = false;
            //player = Resources.Load()
            //rb = this.GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            
            audioManager = GetComponent<AudioManager>();

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
            }
        }

        void PatrolMovement()
        {
            moveSpeed = 10f;
            ifMovingToPlayer = false;
            pointA.position = new Vector3(0,0, 80f);
            pointB.position = new Vector3(70, 3, -70f);
            pointC.position = new Vector3(-70, 0, 0f);

            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

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



        //void PatrolMovement()
        //{
        //    float xMove = Random.Range(0, 20);
        //    float yMove = Random.Range(0, 5);
        //    float zMove = Random.Range(0, 20);

        //    Vector3 position = new Vector3(Random.Range(10, 50), Random.Range(0, 5), Random.Range(10, 50));

        //    Vector3 patrolMovement = new Vector3(position.x + xMove, position.y + yMove, position.z + zMove );
        //    transform.position = Vector3.MoveTowards(transform.position, patrolMovement, moveSpeed * Time.deltaTime);
        //}
        IEnumerator StartMoveToPlayer()
        {
            float timing = 4f;
            yield return new WaitForSeconds(timing);
            ifMovingToPlayer = true;
            moveSpeed = 20;
        }
        void MoveToPlayer()
        {
            //float timing = 4f;
            //yield return new WaitForSeconds(timing);
            
           //if(ifMovingToPlayer)
           // {
                moveSpeed = 20f;
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, angle, 0);
                transform.rotation = rotation;
                transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
           // }

        }

        void OnCollisionEnter(Collision other){
            if (other.gameObject.name == "Player(Clone)")
            {
                //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                ifMovingToPlayer = false;
                gameObject.SetActive(false);
                ColorfullExplosion();

            }
            if (other.gameObject.name == "Bullet(Clone)")
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
            explosion = DestructiblePoolManager.Instance.GetExplosionPieces();
            explosion.transform.position = transform.position;
        }

    }
}