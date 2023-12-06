using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PoliceInspector : MonoBehaviour
{
    private enum Goal
    {
        Wander,
        Inspect,
        SearchForPlayer,
        AttackPlayer,
        MoveToObject
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
        //List<Action> possibleActions = GetPossibleActions();
        //currentPlan = Plan(possibleActions);

        //if (currentPlan.Count > 0)
        //{
        //    currentAction = currentPlan[0];
        //    currentPlan.RemoveAt(0);
        //}
        //else
        //{
        //    currentAction = null;
        //}

        //if (currentAction != null)
        //{
        //    Debug.Log($"Selected Goal: {currentGoal}, Selected Action: {currentAction.Name}");
        //    PerformAction();
        //}
        //else
        //{
        //    Debug.Log($"No action selected for goal: {currentGoal}");
        //    ResetOrChooseNewGoal();
        //}
        if (currentGoal == Goal.Wander && timeSinceLastInspection >= 3f)
        {
            currentGoal = Goal.Inspect;
            timeSinceLastInspection = 0f;
        }
        else if (currentGoal == Goal.Inspect && timeSinceLastInspection >= 2f)
        {
            currentGoal = Goal.SearchForPlayer;
        }
        else if (currentGoal == Goal.SearchForPlayer && timeSinceLastInspection >= 1f)
        {
            currentGoal = Goal.MoveToObject;
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
            else if (currentGoal == Goal.SearchForPlayer && action.Name != "SearchForPlayer")
            {
                validAction = false;
            }
            else if (currentGoal == Goal.MoveToObject && action.Name != "MoveToObject")
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






    //List<Action> GetPossibleActions()
    //{
    //    List<Action> validActions = new List<Action>();

    //    foreach (Action action in actions)
    //    {
    //        bool validAction = true;

    //        foreach (KeyValuePair<string, bool> precondition in action.Preconditions)
    //        {
    //            //Debug.Log($"Inspect Action: {action.Name}, Precondition: {precondition.Key}, Value: {precondition.Value}");
    //            //Debug.Log($"<b>Action: {action.Name}, Precondition: {precondition.Key}, Value: {precondition.Value} </b>");

    //            if (!precondition.Value)
    //            {
    //                validAction = false;
    //                break;
    //            }
    //        }
    //        if (validAction)
    //        {
    //            validActions.Add(action);
    //            //Debug.Log($"<b>CHECKING VALIDATIONS: {validActions.Count}</b>");
    //        }


    //    }
    //    if (validActions.Count > 0)
    //    {
    //        // Choose a random index within the validActions list
    //        int randomIndex = UnityEngine.Random.Range(0, validActions.Count);

    //        // Retrieve the randomly chosen action
    //        Action chosenAction = validActions[randomIndex];

    //        // Perform any additional logic related to the chosen action
    //        //Debug.Log($"<b>Chosen Action: {chosenAction.Name}, Cost: {chosenAction.Cost}</b>");
    //    }

    //    return validActions;
    //}

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

        if (currentAction.Name == "MoveToObject" && Inspecting)
        {
            Inspecting = true;
            //Debug.Log($"<color=blue><b> Executing MoveToObject  </b></color>");

            MoveToObject();//NEEDS TO FIX TELETRANSPORTING BUG CAUSED WHEN CALLING THIS METHOD

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
        //Debug.Log($"<b>Moving to random location...</b>");

        // Set inspection start time when the AI starts moving
        if (!isMoving && !Inspecting)
        {
            StartInspection();
        }

        //Debug.Log($"<b>Moving to random location...</b>");
        //isMoving = true;
        //float moveSpeed = 4f;//The lowest the fastest it will move

        //Vector3 randomPosition = new Vector3(Random.Range(-20f, 30f), 0f, Random.Range(-10f, 40f));
        //StartCoroutine(MoveToPosition(randomPosition, moveSpeed));


        //Debug.Log("Moving to random location...");

        //Debug.Log($"<b>Moving to random location...</b>");
        isMoving = true;
        float moveSpeed = 4f;

        Vector3 randomPosition = new Vector3(Random.Range(-20f, 30f), 0f, Random.Range(-10f, 40f));
        StartCoroutine(MoveToPosition(randomPosition, moveSpeed));
        //Debug.Log($"<b>isMoving set to {isMoving}</b>");

        // Start inspection when the AI starts moving
        //StartInspection();

        // Your existing logic to move the AI to a random location
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

       // Debug.Log($"<b>isMoving set to {isMoving}</b>");
       // Debug.Log($"<color=green><b>Inspecting: {Inspecting}, Time: {Time.time}, Start Time: {inspectionStartTime}, Duration: {inspectionDuration}</b></color>");
        //Debug.Log($"<color=red><b>Inspecting: {Inspecting}, Time: {Time.time}, Start Time: {inspectionStartTime}, Duration: {inspectionDuration}</b></color>");
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
        //Debug.Log($"<b> Here we are going to reset the goal </b>");
        currentGoal = Goal.Wander;
        timeSinceLastInspection = 0f;
    }
    //void GoeToInspect()
    //{
    //    StartCoroutine(MoveToBrokenItem());
    //    inspectionStartTime = Time.time;
    //}
    //IEnumerator MoveToBrokenItem()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Vector3 inspectPosition = bottle.transform.position - transform.position;
    //    transform.Translate(inspectPosition * 5f * Time.deltaTime);
    //}
    // Applying movement 
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

            new Action("SearchForPlayer", 
            new Dictionary<string, bool> { {"AreaInspected", true } }, 
            new Dictionary<string, bool> { {"PlayerFound", true } }),

            new Action("AttackPlayer", 
            new Dictionary<string, bool> { {"PlayerFound", true } }, 
            new Dictionary<string, bool> { {"PlayerAttacked", true } }),

            new Action("MoveToObject",
            new Dictionary<string, bool> { {"AtRandomLocation", true } },
            new Dictionary<string, bool> { {"ObjectReached", true } }) 
        };
    }

    void MoveToObject()
    {
        //Debug.Log($"<b>Moving to object...</b>");
        isMoving = true;
        float moveSpeed = 4f;

        // Replace the following line with the actual position of the object you want to move towards
        Vector3 objectPosition = new Vector3(0f, 0f, 0f);
        transform.LookAt(objectPosition);
        StartCoroutine(MoveToPosition(objectPosition, moveSpeed));

        if (IsInspectionCompleted())
        {
            isMoving = false;
            //Debug.Log("<b>Movement to object completed, isMoving set to False</b>");

            // Wait for 3 seconds before switching to the next action
            StartCoroutine(WaitAndSwitchToNextAction(4f));
        }
        else
        {
            isMoving = true;

            // Set inspection start time when the AI starts moving
            if (!Inspecting)
            {
                inspectionStartTime = Time.time;
            }
        }

        //Debug.Log($"<color=red><b>Inspecting: {Inspecting}, Time: {Time.time}, Start Time: {inspectionStartTime}, Duration: {inspectionDuration}</b></color>");
    }

    IEnumerator WaitAndSwitchToNextAction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Switch to the next action after waiting for the specified time
        SwitchToNextAction();
    }
}
