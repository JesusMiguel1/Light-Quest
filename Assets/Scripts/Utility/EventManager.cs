using System;
using UnityEngine;

namespace object_pool
{
    public class EventManager : MonoBehaviour
    {
        public static event Action SpeedManager;

        public static void SpeedUpdate()
        {
            SpeedManager?.Invoke();
        }
    }
}

