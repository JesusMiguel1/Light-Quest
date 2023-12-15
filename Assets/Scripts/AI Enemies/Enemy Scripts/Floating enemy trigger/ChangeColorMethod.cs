using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeColorMethod : MonoBehaviour
{
    public float colorChangeInterval = 0.5f;
    public Color glowColor = Color.yellow;      // Color to glow
    public float glowIntensity = 2.5f;

    public IEnumerator ChangeColorAndGlow()
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
