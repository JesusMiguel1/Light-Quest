using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour, SpeedControllerResponse
{
    private float patrollingSpeed = 10f;
    private float movingToPlayerSpeed = 20f ;
    public void OnMovingSpeed(float speed)
    {
        patrollingSpeed = speed * 5f;
    }

    public void OnSpeedChanged(float speed)
    {
        movingToPlayerSpeed = speed * 10f;
    }

}
