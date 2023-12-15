using System.Collections;
using UnityEngine;

public class ResizeObject
{
    public float sizeChangeSpeed = 1.0f;
    public IEnumerator LerpSize(Vector3 targetSize, float duration, GameObject gameObject)
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
        gameObject.transform.localScale = targetSize;
    }
}

