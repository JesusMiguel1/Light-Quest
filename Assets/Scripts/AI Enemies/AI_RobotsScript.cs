using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace object_pool
{
    public class AI_RobotsScript : MonoBehaviour
    {
        private enum Goal
        {
            Wander,
            Inspect,
        }

        private class Action
        {
            public string Name;
            public Dictionary<string, bool> Preconditions;
            public Dictionary<string, bool> Effects;
            public float Cost;

            public Action(string name, Dictionary<string, bool> preconditions, Dictionary<string, bool> effects, float cost = 1)
            {
                Name = name;
                Preconditions = preconditions;
                Effects = effects;
                Cost = cost;
            }
        }

        private List<Action> actions;
        private List<Action> currentPlan;
        private Action currentAction;
        private Goal currentGoal;
        private float timeSinceLastInspection;
        private bool isMoving = false;
        private bool Inspecting = false;
        private float timeToSwitchAction = 1f;
        private float inspectionStartTime; // New variable
        private float inspectionDuration = 5f;
        [SerializeField] GameObject bottle;

        void Start()
        {
            actions = CreateActions();
            currentPlan = new List<Action>();
            currentAction = null;
            currentGoal = Goal.Wander;
            timeSinceLastInspection = 0f;
            inspectionStartTime = 0f;
        }

        void Update()
        {

            timeSinceLastInspection += Time.deltaTime;
            if (timeSinceLastInspection >= 10f) // Adjust this time as needed
            {
                SwitchToNextAction();
            }

            ChooseAction();
            if (currentAction != null)
            {
                PerformAction();
            }
        }

        void ChooseAction()
        {
            Debug.Log($"<color=yellow><b>My Selected Goal: {currentGoal}, Selected Action: {currentAction?.Name}</b></color>");
            
            if (currentGoal == Goal.Wander && timeSinceLastInspection >= 3f)
            {
                currentGoal = Goal.Inspect;
                timeSinceLastInspection = 0f;
            }
            List<Action> possibleActions = GetPossibleActions();
            currentPlan = Plan(possibleActions);

            if (currentPlan.Count > 0)
            {
                currentAction = currentPlan[0];
                currentPlan.RemoveAt(0);
            }
            else
            {
                currentAction = null;
            }

            if (currentAction != null)
            {
                Debug.Log($"<color=yellow><b>Selected Goal: {currentGoal}, Selected Action: {currentAction.Name}</b></color>");
                PerformAction();
            }
            else
            {
                Debug.Log($"<color=yellow><b>No action selected for goal: {currentGoal}</b></color>");
                ResetOrChooseNewGoal();
            }
        }

        List<Action> Plan(List<Action> possibleActions)
        {
            return new List<Action>(possibleActions);
        }
        List<Action> GetPossibleActions()
        {
            List<Action> validActions = new List<Action>();

            foreach (Action action in actions)
            {
                bool validAction = true;

                // Check if the action is valid for the current goal
                if (currentGoal == Goal.Wander && action.Name != "Wander")
                {
                    validAction = false;
                }
                else if (currentGoal == Goal.Inspect && action.Name != "Inspect")
                {
                    validAction = false;
                }

                // Check additional preconditions
                foreach (KeyValuePair<string, bool> precondition in action.Preconditions)
                {
                    if (!precondition.Value)
                    {
                        validAction = false;
                        break;
                    }
                }

                if (validAction)
                {
                    validActions.Add(action);
                }
            }

            return validActions;
        }

        void PerformAction()
        {
            //Debug.Log($"<b>Performing action: {currentAction.Name}</b>");

            foreach (KeyValuePair<string, bool> effect in currentAction.Effects)
            {
                UpdateAIState(effect.Key, effect.Value);
            }

            if (currentAction.Name == "Wander" && !isMoving)
            {
                // Debug.Log($"<color=yellow><b> Executing Wander  </b></color>");
                MoveToRandomLocation();
            }

            if (currentAction.Name == "Inspect" && Inspecting)
            {
                //Debug.Log($"<color=green><b> Executing Inspect  </b></color>");
                Inspecting = true;
            }

            // Check if inspection is completed and switch to the next action
            if (Inspecting && IsInspectionCompleted())
            {
                Inspecting = false;
                //Debug.Log($"<color=red><b> Switching to the next action </b></color>");
                SwitchToNextAction();
            }
        }


        void UpdateAIState(string key, bool value)
        {
            //Here we will add conditions to indicate the state 
            //if(key == "Inspect") 
            //{
            //    Inspecting = value;
            //}
        }
        void MoveToRandomLocation()
        {
            if (!isMoving && !Inspecting)
            {
                StartInspection();
            }


            isMoving = true;
            float moveSpeed = 4f;

            Vector3 randomPosition = new Vector3(Random.Range(-20f, 30f), 2f, Random.Range(-10f, 40f));
            StartCoroutine(MoveToPosition(randomPosition, moveSpeed));

            if (!Inspecting)
            {
                inspectionStartTime = Time.time;
            }

            // Assuming IsInspectionCompleted() represents the condition for movement completion
            if (IsInspectionCompleted())
            {
                isMoving = false;
                //Debug.Log($"<color=yellow><b>isMoving set to {isMoving} if completed</b></color>");
                currentGoal = Goal.Inspect;
                StartInspection();
            }
            else
            {
                isMoving = true;
                if (!Inspecting)
                {
                    inspectionStartTime = Time.time;
                }
            } 
        }

        void StartInspection()
        {
            Inspecting = true;
            inspectionStartTime = Time.time;
            //Debug.Log($"<color=yellow><b>Inspecting set to {Inspecting}</b></color>");
        }
        void SwitchToNextAction()
        {
            Inspecting = false;
            //Debug.Log("<b>Switching to the next action</b>");
            if (currentPlan.Count > 0)
            {
                currentAction = currentPlan[0];
                currentPlan.RemoveAt(0);
            }
            else
            {
                ResetOrChooseNewGoal();
            }
        }

        void ResetOrChooseNewGoal()
        {
            currentGoal = Goal.Wander;
            timeSinceLastInspection = 0f;
        }


        IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            float timeElapsed = 0f;
            float rotationSpeed = 9f;
            Vector3 startPosition = transform.position;

            while (timeElapsed < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

                Vector3 rotationToTarget = targetPosition - transform.position;
                float angle = Mathf.Atan2(rotationToTarget.x, rotationToTarget.z) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            isMoving = false;
        }

        bool IsInspectionCompleted()
        {
            float elapsedTime = Time.time - inspectionStartTime;
            //Debug.Log($"<color=blue><b>Inspecting: {Inspecting}, Time: {Time.time}, Start Time: {inspectionStartTime}, Elapsed Time: {elapsedTime}, Duration: {inspectionDuration}</b></color>");
            return Inspecting && elapsedTime > inspectionDuration;
        }

        List<Action> CreateActions()
        {
            return new List<Action>
        {
            new Action("Wander",
            new Dictionary<string, bool>(),
            new Dictionary<string, bool> { {"AtRandomLocation", true } }),

            new Action("Inspect",
            new Dictionary<string, bool> { {"AtRandomLocation", true } },
            new Dictionary<string, bool> { {"AreaInspected", true } }),

        };
        }

        IEnumerator WaitAndSwitchToNextAction(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            // Switch to the next action after waiting for the specified time
            SwitchToNextAction();
        }










        //public float moveSpeed = 5f;
        //private float rotationSpeed = 20;
        //private float detectPlayerRadius = 0.1f;
        //public bool ifMovingToPlayer = false;
        //Vector3 movement;
        //[SerializeField] Transform player; // Reference to the player's transform
        //Rigidbody rb;

        //GameObject wayPointPrefab;
        //GameObject explosion;

        //public AudioClip audioClips;
        //public AudioClip policeAudioClips;
        //AudioSource audioSource;

        //private AudioManager audioManager;

        //void OnEnable()
        //{
        //    audioSource = GetComponent<AudioSource>();
        //    if (audioSource != null && policeAudioClips != null)
        //    {
        //        audioSource.volume = 0.5f;
        //        audioSource.PlayOneShot(policeAudioClips);
        //    }

        //}
        //void Start()
        //{

        //    rb = this.GetComponent<Rigidbody>();

        //    if (player == null)
        //    {
        //        player = GameObject.FindWithTag("Player").transform;
        //    }
        //    audioManager = GetComponent<AudioManager>();

        //    // Initialize patrol direction
        //}

        //void Update()
        //{


        //    if (!ifMovingToPlayer)
        //    {
        //        float randomX = Random.Range(0f, 20f);
        //        float randomY = Random.Range(0f, 5f);
        //        float randomZ = Random.Range(20f, 0f);

        //        Vector3 newPosition = new Vector3(randomX, randomY, randomZ);
        //        transform.Translate(newPosition * moveSpeed * Time.deltaTime);
        //    }


        //    StartCoroutine(MoveToPlayer());


        //}


        //#region Move towards player 
        //IEnumerator MoveToPlayer()
        //{
        //    float randomTime = Random.Range(0.1f, 4.1f);
        //    yield return new WaitForSeconds(randomTime);
        //    ifMovingToPlayer = true;
        //    Vector3 moveDirection = player.position - transform.position;
        //    if (moveDirection.magnitude >= detectPlayerRadius && ifMovingToPlayer)
        //    {
        //        moveSpeed = Random.Range(10f, 25f);
        //        // Rotate towards the player
        //        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //        Vector3 direction = player.position - transform.position;
        //        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        //        Quaternion rotation = Quaternion.Euler( 0f, angle, 0f);
        //        transform.rotation = rotation;  

        //        // Move towards the player
        //        movement = moveDirection.normalized;
        //    }

        //}
        //#endregion


        //#region Fixed update move the drone
        //private void FixedUpdate()
        //{
        //    MoveDroneTowardsPlayer(movement);
        //}

        //void MoveDroneTowardsPlayer(Vector3 direction)
        //{
        //    // Move the drone using rigidbody
        //    rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        //}
        //#endregion

        //void OnCollisionEnter(Collision other)

        //{
        //    if (other.gameObject.name == "Player(Clone)")
        //    {
        //        //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
        //        gameObject.SetActive(false);
        //        ColorfullExplosion();

        //    }
        //    if (other.gameObject.name == "Bullet(Clone)")
        //    {
        //        AudioManager.Instance.PlayOneShot(audioClips);
        //        //// int clipsIndex = Random.Range(0, audioClips.Length);
        //        //if (audioManager != null)
        //        //{
        //        //    audioManager.PlayOneShot(audioClips);
        //        //}
        //    }
        //}

        //void ColorfullExplosion()
        //{
        //    explosion = StrikePlayerExplosionPool.Instance.GetExplosionPieces();
        //    explosion.transform.position = transform.position;

        //}
        ////    public float moveSpeed = 5f;
        ////    private float rotationSpeed = 20;
        ////    private float detectPlayerRadius = 0.1f;
        ////    public bool ifMovingToPlayer = false;
        ////    Vector3 movement;
        ////    [SerializeField] Transform player; // Reference to the player's transform
        ////    Rigidbody rb;

        ////    GameObject wayPointPrefab;
        ////    GameObject explosion;

        ////    public AudioClip audioClips;
        ////    public AudioClip policeAudioClips;
        ////    AudioSource audioSource;

        ////    private AudioManager audioManager;


        ////    void Start()
        ////    {
        ////        moveSpeed = UnityEngine.Random.Range(20f, 30f);
        ////        rb = this.GetComponent<Rigidbody>();
        ////        audioSource = GetComponent<AudioSource>();

        ////        //if (player == null)
        ////        //{
        ////        //    //player = GameObject.FindGameObjectWithTag("Player").transform;
        ////        //}
        ////        audioManager = GetComponent<AudioManager>();

        ////        // Initialize patrol direction
        ////    }

        ////    void Update()
        ////    {
        ////        StartCoroutine(MoveToPlayer());

        ////        if (audioSource != null && policeAudioClips != null)
        ////        {
        ////            audioSource.volume = 0.03f;
        ////            audioSource.PlayOneShot(policeAudioClips);
        ////        }
        ////    }

        ////    IEnumerator MoveToPlayer()
        ////    {
        ////        float randomTime = Random.Range(0.1f, 4.1f);
        ////        yield return new WaitForSeconds(randomTime);

        ////        ifMovingToPlayer = true;

        ////        TransformAccessArray transforms = new TransformAccessArray(1);
        ////        transforms.Add(transform);

        ////        JobHandle jobHandle;

        ////        try
        ////        {
        ////            MoveToPlayerJob job = new MoveToPlayerJob
        ////            {
        ////                deltaTime = Time.deltaTime,
        ////                detectPlayerRadius = detectPlayerRadius,
        ////                rotationSpeed = rotationSpeed,
        ////                playerPosition = player.position,
        ////                moveSpeed = moveSpeed // Set the moveSpeed parameter
        ////            };

        ////            jobHandle = job.Schedule(transforms);
        ////            jobHandle.Complete();
        ////        }
        ////        finally
        ////        {
        ////            transforms.Dispose();
        ////        }
        ////    }
        ////    #region
        ////    void OnCollisionEnter(Collision other)

        ////    {
        ////        if (other.gameObject.name == "Player")
        ////        {
        ////            //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
        ////            gameObject.SetActive(false);
        ////            ColorfullExplosion();

        ////        }
        ////        if (other.gameObject.name == "Bullet(Clone)")
        ////        {
        ////            AudioManager.Instance.PlayOneShot(audioClips);
        ////            //// int clipsIndex = Random.Range(0, audioClips.Length);

        ////        }
        ////    }
        ////    #endregion
        ////    void ColorfullExplosion()
        ////    {
        ////        explosion = DestructiblePoolManager.Instance.GetExplosionPieces();
        ////        explosion.transform.position = transform.position;

        ////    }

    }
    }
