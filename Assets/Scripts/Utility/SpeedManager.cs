using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour, SpeedControllerResponse
{
    float currentSpeed;
    public void OnMovingSpeed(float speed)
    { 
        currentSpeed = speed;
    }

    public float OnSpeedChanged(float speed)
    {
       return currentSpeed = speed; ;
    }

}
