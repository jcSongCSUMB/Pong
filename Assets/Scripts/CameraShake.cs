using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Mathf.PerlinNoise(Time.time * magnitude, 0.0f) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(0.0f, Time.time * magnitude) * 2.0f - 1.0f;
            transform.localPosition = new Vector3(x, y, originalPosition.z) * magnitude;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
