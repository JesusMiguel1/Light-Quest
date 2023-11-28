using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace punk_vs_robots
{
    public class InitialDrone : MonoBehaviour
    {
        //Floating variables
        private Vector3 InitialPosition;
        private float floatingHorizontalOffset = 0.0f;
        private float floatingHorizontalDistance = 0.3f;
        private float verticalPositionOffset;
        private float floatingHeight = 0.3f;
        private float floatSpeed = 1.0f;

        public float sizeChangeSpeed = 1.0f;
        public bool droneDestroyed;

        public float minSize = 2.35f;
        public float maxSize = 3f;

        public float colorChangeInterval = 0.5f;  // Time interval for color change
        public Color glowColor = Color.yellow;      // Color to glow
        public float glowIntensity = 2.5f;        // Intensity of the glow

        // Start is called before the first frame update
        void Start()
        {
            //enemies = GetComponent<ActivateEnemies>();
            InitialPosition = transform.position;
            StartCoroutine(ContinuousPulse());
            StartCoroutine(ChangeColorAndGlow());
        }

        void Update()
        {
            FloatingAI();
            
        }
        //void LateUpdate() { ChangeOrbSize(); }
        private void FloatingAI()
        {
            //Floating calculation 
            verticalPositionOffset = Mathf.Sin(Time.time * floatSpeed) * floatingHeight;
            floatingHorizontalOffset = Mathf.Sin(Time.time * floatSpeed * 1.5f) * floatingHorizontalDistance;

            Vector3 newPosition = InitialPosition + Vector3.up * verticalPositionOffset + Vector3.right * floatingHorizontalOffset;
            transform.position = newPosition;

        }
        IEnumerator ContinuousPulse()
        {
            while (true)
            {
                // Generate a random target size within the pulsating range
                Vector3 targetSize = Vector3.one * Random.Range(maxSize, minSize);

                // Start a coroutine to smoothly change the size over time
                yield return StartCoroutine(LerpSize(targetSize, sizeChangeSpeed));
            }
        }

        IEnumerator LerpSize(Vector3 targetSize, float duration)
        {
            float elapsedTime = 0f;
            Vector3 initialSize = gameObject.transform.localScale;

            while (elapsedTime < duration)
            {
                // Interpolate between the initial size and the target size
                float lerpTime = elapsedTime / duration;
                float size = Mathf.Lerp(initialSize.magnitude, targetSize.magnitude, Mathf.PingPong(lerpTime * sizeChangeSpeed, 1.5f));
                gameObject.transform.localScale = Vector3.one * size;

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Ensure that the final size is set exactly to the target size
            gameObject.transform.localScale = targetSize;
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Bullet")
            {
                //spawner.DroneDestroyed = true;
                //enemies.isFirstDroneDestroyed = true;
                droneDestroyed = true;
                Debug.Log($"<b>Collision With bullet </b> <color=blue> <b>{other.gameObject.name}</b> </color>");
            }
        }

        IEnumerator ChangeColorAndGlow()
        {
            while (true)
            {
                // Change color
                ChangeColor(Random.ColorHSV());

                // Wait for a specified interval
                yield return new WaitForSeconds(colorChangeInterval);

                // Restore original color
                ChangeColor(Color.white);

                // Wait for a short duration before starting the next cycle
                yield return new WaitForSeconds(0.5f);
            }
        }

        void ChangeColor(Color targetColor)
        {
            StartCoroutine(LerpColor(targetColor, 1.0f));
        }

        IEnumerator LerpColor(Color targetColor, float duration)
        {
            float elapsedTime = 0f;
            Color initialColor = GetComponent<Renderer>().material.color;

            while (elapsedTime < duration)
            {
                // Interpolate between the initial color and the target color
                GetComponent<Renderer>().material.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }

            // Ensure that the final color is set exactly to the target color
            GetComponent<Renderer>().material.color = targetColor;

            // Apply the glow effect
            StartCoroutine(Glow(glowColor, glowIntensity));
        }

        IEnumerator Glow(Color glowColor, float intensity)
        {
            Material material = GetComponent<Renderer>().material;

            Color originalColor = material.color;
            material.EnableKeyword("_EMISSION");

            // Set the emission color to create a glow effect
            material.SetColor("_EmissionColor", glowColor * intensity);

            // Wait for a short duration
            yield return new WaitForSeconds(0.5f);

            // Reset the emission color to the original color
            material.SetColor("_EmissionColor", originalColor * intensity);

            // Disable emission to turn off the glow effect
            material.DisableKeyword("_EMISSION");
        }
    }

}