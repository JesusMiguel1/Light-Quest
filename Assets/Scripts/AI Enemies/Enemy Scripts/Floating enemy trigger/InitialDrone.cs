using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace punk_vs_robots
{
    public class InitialDrone : MonoBehaviour
    {
       
        //Other classes
        FloatingAITrigger floatingAI;
        ChangeColorMethod colorChange;
        ResizeObject pump;

        #region VARIABLES
        private Vector3 InitialPosition;
        public float sizeChangeSpeed = 1.0f;
        public float minSize = 2.35f;
        public float maxSize = 3f;
        public bool droneDestroyed;
        #endregion

        void Start()
        {
            floatingAI = new FloatingAITrigger();
            pump = new ResizeObject();
            colorChange = GetComponent<ChangeColorMethod>();

            InitialPosition = transform.position;
            StartCoroutine(ContinuousPulse());
            StartCoroutine(colorChange.ChangeColorAndGlow());
        }

        void Update()
        {
            floatingAI.FloatingAI(InitialPosition, transform);
        }
        
        IEnumerator ContinuousPulse()
        {
            while (true)
            {
                // Generate a random target size within the pulsating range
                Vector3 targetSize = Vector3.one * Random.Range(maxSize, minSize);

                // Start a coroutine to smoothly change the size over time
                yield return StartCoroutine(pump.LerpSize(targetSize, sizeChangeSpeed, gameObject));
            }
        }
 
    }
}

