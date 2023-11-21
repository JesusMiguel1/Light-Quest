using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface SpeedControllerResponse
{
    void OnMovingSpeed(float speed);
    float OnSpeedChanged(float speed);
}


