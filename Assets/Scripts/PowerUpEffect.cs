using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float blinkSpeed = 1f;
    private Renderer powerUpRenderer;
    private Color originalColor;
    private float blinkTimer;

    void Start()
    {
        powerUpRenderer = GetComponent<Renderer>();
        originalColor = powerUpRenderer.material.color;
        blinkTimer = 0f;
    }

    void Update()
    {
        // Rotate the power-up object around the Z axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);

        // Blink effect with smooth transition
        blinkTimer += Time.deltaTime * blinkSpeed;
        float lerpFactor = Mathf.PingPong(blinkTimer, 1f);
        powerUpRenderer.material.color = Color.Lerp(originalColor, Color.black, lerpFactor);
    }
}
