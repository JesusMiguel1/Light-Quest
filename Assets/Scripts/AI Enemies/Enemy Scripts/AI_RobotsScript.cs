using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

    public class AI_RobotsScript : MonoBehaviour
    {
        #region VARIABLES
        private float movingSpeed;
        private float rotationSpeed;

        private bool isWandering = false;
        private bool isRotatingRight = false;
        private bool isRotatingLeft = false;
        private bool isWalking = false;
        public GameObject speedTrigger;
        private GlobalStrings strings;
        private HashSet<string> obstruction;
        #endregion


        #region START AND UPDATE 
        void Start()
        {
            strings = new GlobalStrings();
            movingSpeed = 3f;
            rotationSpeed = 100.0f;
            speedTrigger = GameObject.Find(strings.EnemyTrigger);
            obstruction = new HashSet<string>
            {
                "Cube (1)", "Cube (2)", "Cube (3)","Cube (4)", "Cube (5)", "Cube (6)","Cube (7)", "Cube (8)","R1_Enemy(Clone)"
            };
        }

        void Update()
        {
            NPCWander();
            Debug.Log($"<b> WHAT ENEMY IS PRINTING{speedTrigger.name}</b>");
            if (!speedTrigger.activeInHierarchy)
            {
                movingSpeed = 20;
            }
        }
        #endregion


        #region NPC WANDER CONDITIONS
        public void NPCWander() 
        {
            AvoidObstacles(); // New line added for obstacle avoidance

            if (isWandering == false)
                StartCoroutine(Wander());
            if (isRotatingRight == true)
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            if (isRotatingLeft == true)
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
            if (isWalking == true)
                transform.position += transform.forward * movingSpeed * Time.deltaTime;
        }
        #endregion


        #region WANDER COROUTINE METHOD
        IEnumerator Wander()
        {
            int timeToRotate = Random.Range(1, 3);
            int waitAndRotate = Random.Range(0, 1);
            int walkTimeRange = Random.Range(1, 5);
            int waitBeforWalk = Random.Range(0, 1);
            int leftOrRightRotation = Random.Range(1, 2);

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
        #endregion


        #region VOID OBSTACLES
        void AvoidObstacles()
        {
            RaycastHit hit;
            float rayLength = 2.0f; // Adjust as needed

            // Cast a ray in front of the robot
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                // If an obstacle is detected, adjust the rotation to avoid it
                //Debug.Log($"<b> OBJECT HIT BY THE RAYCAST {hit.collider.name} </b>");
                if(obstruction.Contains(hit.collider.name))
                    transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            }
        }
        #endregion



        /**** WANDER METHOD USING GOAP ****/
        //  private enum Goal
        //  {
        //      Wander,
        //      Inspect,
        //  }

        //  private class Action
        //  {
        //      public string Name;
        //      public Dictionary<string, bool> Preconditions;
        //      public Dictionary<string, bool> Effects;
        //      public float Cost;

        //      public Action(string name, Dictionary<string, bool> preconditions, Dictionary<string, bool> effects, float cost = 1)
        //      {
        //          Name = name;
        //          Preconditions = preconditions;
        //          Effects = effects;
        //          Cost = cost;
        //      }
        //  }

        //  private List<Action> actions;
        //  private List<Action> currentPlan;
        //  private Action currentAction;
        //  private Goal currentGoal;
        //  private float timeSinceLastInspection;
        //  private bool isMoving = false;
        //  private bool Inspecting = false;
        //  private float timeToSwitchAction = 1f;
        //  private float inspectionStartTime; // New variable
        //  private float inspectionDuration = 5f;

        //  void Start()
        //  {
        //      actions = CreateActions();
        //      currentPlan = new List<Action>();
        //      currentAction = null;
        //      currentGoal = Goal.Wander;
        //      timeSinceLastInspection = 0f;
        //      inspectionStartTime = 0f;
        //  }

        //  void Update()
        //  {
        //      timeSinceLastInspection += Time.deltaTime;
        //      if (timeSinceLastInspection >= 10f) // Adjust this time as needed
        //      {
        //          SwitchToNextAction();
        //      }

        //      ChooseAction();
        //      if (currentAction != null)
        //      {
        //          PerformAction();
        //      }
        //  }

        //  void ChooseAction()
        //  {
        //      Debug.Log($"<color=yellow><b>My Selected Goal: {currentGoal}, Selected Action: {currentAction?.Name}</b></color>");

        //      if (currentGoal == Goal.Wander && timeSinceLastInspection >= 3f)
        //      {
        //          currentGoal = Goal.Inspect;
        //          timeSinceLastInspection = 0f;
        //      }
        //      List<Action> possibleActions = GetPossibleActions();
        //      currentPlan = Plan(possibleActions);

        //      if (currentPlan.Count > 0)
        //      {
        //          currentAction = currentPlan[0];
        //          currentPlan.RemoveAt(0);
        //      }
        //      else
        //      {
        //          currentAction = null;
        //      }

        //      if (currentAction != null)
        //      {
        //          Debug.Log($"<color=yellow><b>Selected Goal: {currentGoal}, Selected Action: {currentAction.Name}</b></color>");
        //          PerformAction();
        //      }
        //      else
        //      {
        //          Debug.Log($"<color=yellow><b>No action selected for goal: {currentGoal}</b></color>");
        //          ResetOrChooseNewGoal();
        //      }
        //  }

        //  List<Action> Plan(List<Action> possibleActions)
        //  {
        //      return new List<Action>(possibleActions);
        //  }
        //  List<Action> GetPossibleActions()
        //  {
        //      List<Action> validActions = new List<Action>();

        //      foreach (Action action in actions)
        //      {
        //          bool validAction = true;

        //          // Check if the action is valid for the current goal
        //          if (currentGoal == Goal.Wander && action.Name != "Wander")
        //          {
        //              validAction = false;
        //          }
        //          else if (currentGoal == Goal.Inspect && action.Name != "Inspect")
        //          {
        //              validAction = false;
        //          }

        //          // Check additional preconditions
        //          foreach (KeyValuePair<string, bool> precondition in action.Preconditions)
        //          {
        //              if (!precondition.Value)
        //              {
        //                  validAction = false;
        //                  break;
        //              }
        //          }

        //          if (validAction)
        //          {
        //              validActions.Add(action);
        //          }
        //      }

        //      return validActions;
        //  }

        //  void PerformAction()
        //  {
        //      //Debug.Log($"<b>Performing action: {currentAction.Name}</b>");

        //      foreach (KeyValuePair<string, bool> effect in currentAction.Effects)
        //      {
        //          UpdateAIState(effect.Key, effect.Value);
        //      }

        //      if (currentAction.Name == "Wander" && !isMoving)
        //      {
        //          // Debug.Log($"<color=yellow><b> Executing Wander  </b></color>");
        //          MoveToRandomLocation();
        //      }

        //      if (currentAction.Name == "Inspect" && Inspecting)
        //      {
        //          //Debug.Log($"<color=green><b> Executing Inspect  </b></color>");
        //          Inspecting = true;
        //      }

        //      // Check if inspection is completed and switch to the next action
        //      if (Inspecting && IsInspectionCompleted())
        //      {
        //          Inspecting = false;
        //          //Debug.Log($"<color=red><b> Switching to the next action </b></color>");
        //          SwitchToNextAction();
        //      }
        //  }


        //  void UpdateAIState(string key, bool value)
        //  {
        //      //Here we will add conditions to indicate the state 
        //      //if(key == "Inspect") 
        //      //{
        //      //    Inspecting = value;
        //      //}
        //  }
        //  void MoveToRandomLocation()
        //  {
        //      if (!isMoving && !Inspecting)
        //      {
        //          StartInspection();
        //      }


        //      isMoving = true;
        //      float moveSpeed = 4f;

        //      Vector3 randomPosition = new Vector3(Random.Range(-20f, 30f), transform.position.y, Random.Range(-10f, 40f));
        //      StartCoroutine(MoveToPosition(randomPosition, moveSpeed));

        //      if (!Inspecting)
        //      {
        //          inspectionStartTime = Time.time;
        //      }

        //      // Assuming IsInspectionCompleted() represents the condition for movement completion
        //      if (IsInspectionCompleted())
        //      {
        //          isMoving = false;
        //          //Debug.Log($"<color=yellow><b>isMoving set to {isMoving} if completed</b></color>");
        //          currentGoal = Goal.Inspect;
        //          StartInspection();
        //      }
        //      else
        //      {
        //          isMoving = true;
        //          if (!Inspecting)
        //          {
        //              inspectionStartTime = Time.time;
        //          }
        //      } 
        //  }

        //  void StartInspection()
        //  {
        //      Inspecting = true;
        //      inspectionStartTime = Time.time;
        //      //Debug.Log($"<color=yellow><b>Inspecting set to {Inspecting}</b></color>");
        //  }
        //  void SwitchToNextAction()
        //  {
        //      Inspecting = false;
        //      //Debug.Log("<b>Switching to the next action</b>");
        //      if (currentPlan.Count > 0)
        //      {
        //          currentAction = currentPlan[0];
        //          currentPlan.RemoveAt(0);
        //      }
        //      else
        //      {
        //          ResetOrChooseNewGoal();
        //      }
        //  }

        //  void ResetOrChooseNewGoal()
        //  {
        //      currentGoal = Goal.Wander;
        //      timeSinceLastInspection = 0f;
        //  }


        //  IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        //  {
        //      float timeElapsed = 0f;
        //      float rotationSpeed = 9f;
        //      Vector3 startPosition = transform.position;

        //      while (timeElapsed < duration)
        //      {
        //          transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);

        //          Vector3 rotationToTarget = targetPosition - transform.position;
        //          float angle = Mathf.Atan2(rotationToTarget.x, rotationToTarget.z) * Mathf.Rad2Deg;
        //          Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

        //          transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        //          timeElapsed += Time.deltaTime;
        //          yield return null;
        //      }

        //      transform.position = targetPosition;
        //      isMoving = false;
        //  }

        //  bool IsInspectionCompleted()
        //  {
        //      float elapsedTime = Time.time - inspectionStartTime;
        //      //Debug.Log($"<color=blue><b>Inspecting: {Inspecting}, Time: {Time.time}, Start Time: {inspectionStartTime}, Elapsed Time: {elapsedTime}, Duration: {inspectionDuration}</b></color>");
        //      return Inspecting && elapsedTime > inspectionDuration;
        //  }

        //  List<Action> CreateActions()
        //  {
        //      return new List<Action>
        //  {
        //      new Action("Wander",
        //      new Dictionary<string, bool>(),
        //      new Dictionary<string, bool> { {"AtRandomLocation", true } }),

        //      new Action("Inspect",
        //      new Dictionary<string, bool> { {"AtRandomLocation", true } },
        //      new Dictionary<string, bool> { {"AreaInspected", true } }),

        //  };
        //}

        //  IEnumerator WaitAndSwitchToNextAction(float waitTime)
        //  {
        //      yield return new WaitForSeconds(waitTime);

        //      // Switch to the next action after waiting for the specified time
        //      SwitchToNextAction();
        //  }
    }

