using System.Collections;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Jobs;
using UnityEngine.UIElements;


    public class AI_Drones : MonoBehaviour
    {
        public float rotationSpeed = 180.0f;
        public float spiralRadius = 25.0f; // Adjust the radius as needed
        public float spiralSpeed;
        //private float timer;
        //private float changeDirectionInterval = 1.0f; // Change direction every 3 seconds

        private float droneDuration = 10;
        private float dronefeTime;


        //Patrolling
        //private float rotationSpeed;
        private float patrolSpeed;
        private bool isWandering = false;
        private bool isRotatingRight = false;
        private bool isRotatingLeft = false;
        private bool isWalking = false;

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
            spiralSpeed = Random.Range(1f, 3f);
            patrolSpeed = 15f;
            rotationSpeed = 100f;
            StartCoroutine(FlashingLightSiren());
            audioSource = GetComponent<AudioSource>();

            audioManager = GetComponent<AudioManager>();
            if (audioSource != null && policeAudioClips != null)
            {
                audioSource.volume = 1f;
                audioSource.PlayOneShot(policeAudioClips);
            }
            speed = new GlobalSpeedManager();
            dronefeTime = droneDuration;
        }
        void Start()
        {
            
            ifMovingToPlayer = false;
            //player = Resources.Load()
            //rb = this.GetComponent<Rigidbody>();
            //SetNewTarget();
        }

        void Update()
        {
            
            if (!ifMovingToPlayer)
            {
                //PatrolMovement();
                NPCWander();
            }
            
            StartCoroutine(StartMoveToPlayer());

            if (ifMovingToPlayer)
            {
                MoveToPlayer();
                speed.CurrentSpeed = 50f;
            }
            SelfDestruction();
        }
        void SelfDestruction()
        {
            // Destroy drone is too far from player too keep the wave going
            float distance = Vector3.Distance(player.position, transform.position);
            
            if(distance < 5) {
                Debug.Log($"<b> CHECKING THE DISTANCE BETWEEN PLAYER AND DRONES{distance}</b>");

                //HERE WE CAN INCLUDE POLICE SCORES, EVERYTIME THEY HIT THE PLAYER 
                gameObject.SetActive(false);
            }
                
            //dronefeTime -= Time.deltaTime;
            //if(dronefeTime > 0)
            //{
            //    
            //}
        }
        //void PatrolMovement()
        //{
        //    //moveSpeed = speed.CurrentSpeed;
        //    ifMovingToPlayer = false;
        //    pointA.position = new Vector3(0,0, 50f);
        //    pointB.position = new Vector3(40, 3, -40f);
        //    pointC.position = new Vector3(-40, 0, 0f);

        //    transform.position = Vector3.MoveTowards(transform.position, target.position, speed.CurrentSpeed * Time.deltaTime);

        //    Vector3 direction = target.position - transform.position;
        //    float angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
        //    Quaternion rotation = Quaternion.Euler(0, angle, 0);
        //    transform.rotation = rotation;


        //    // Check if the object reaches the target position
        //    if (Vector3.Distance(transform.position, target.position) < 0.1f)
        //    {
        //        SetNewTarget();
        //    }
        //}

        //void SetNewTarget()
        //{
        //    // Randomly select the next target point (A, B, or C)
        //    int randomIndex = Random.Range(0, 3);
        //    if (randomIndex == 0)
        //        target = pointA;
        //    else if (randomIndex == 1)
        //        target = pointB;
        //    else
        //        target = pointC;
        //}

        IEnumerator StartMoveToPlayer()
        {
            float timing = 3f;
            yield return new WaitForSeconds(timing);
            ifMovingToPlayer = true;
            moveSpeed = speed.CurrentSpeed;
        }

        void MoveToPlayer()
        {
            moveSpeed = speed.CurrentSpeed;


            if (player != null)
            {
                // spiral movement
                float currentAngle = Time.time * spiralSpeed;
                float xSpiral = Mathf.Sin(currentAngle) * spiralRadius;
                float zSpiral = Mathf.Sin(currentAngle) * spiralRadius;

                Vector3 spiralPosition = new Vector3(xSpiral, 4, zSpiral);
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.y = 5;
                directionToPlayer.Normalize();

                Vector3 targetPosition = spiralPosition + directionToPlayer * moveSpeed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                float playerAngle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
                Quaternion toRotation = Quaternion.Euler(0, playerAngle, 0);
                transform.rotation = toRotation;
            }
            //Previos code

            //{
            //    moveSpeed = speed.CurrentSpeed;
            //    Vector3 direction = player.position - transform.position;
            //    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //    Quaternion rotation = Quaternion.Euler(0, angle, 0);
            //    transform.rotation = rotation;
            //    transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);

            //}
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
        public void NPCWander()
        {
            if (isWandering == false)
                StartCoroutine(Wander());
            if (isRotatingRight == true)
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            if (isRotatingLeft == true)
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
            if (isWalking == true)
                transform.position += transform.forward * patrolSpeed * Time.deltaTime;
        }
        IEnumerator Wander()
        {
            int timeToRotate = Random.Range(0, 1);
            int waitAndRotate = Random.Range(0, 1);
            int walkTimeRange = Random.Range(1, 3);
            int waitBeforWalk = Random.Range(0,0);
            int leftOrRightRotation = Random.Range(0, 1);

            isWandering = true;

            yield return new WaitForSeconds(waitBeforWalk);
            isWalking = true;

            yield return new WaitForSeconds(walkTimeRange);
            isWalking = false;

            yield return new WaitForSeconds(waitAndRotate);

            if (leftOrRightRotation == 1)
            {
                isRotatingRight = true;
                yield return new WaitForSeconds(timeToRotate);
                isRotatingRight = false;
            }

            if (leftOrRightRotation == 2)
            {
                isRotatingLeft = true;
                yield return new WaitForSeconds(timeToRotate);
                isRotatingLeft = false;
            }
            isWandering = false;
        }
    }
