using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float blinkSpeed = 1f;
    public float moveSpeed = 1f; // Speed of circular motion
    public float radius = 5f; // Radius of circular motion

    private Renderer powerUpRenderer;
    private Color originalColor;
    private float blinkTimer;
    private float angle;

    void Start()
    {
        powerUpRenderer = GetComponent<Renderer>();
        originalColor = powerUpRenderer.material.color;
        blinkTimer = 0f;

        // Calculate initial angle based on position
        angle = Mathf.Atan2(transform.position.y, transform.position.x);
    }

    void Update()
    {
        // Rotate the power-up object around the Z axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);

        // Blink effect with smooth transition
        blinkTimer += Time.deltaTime * blinkSpeed;
        float lerpFactor = Mathf.PingPong(blinkTimer, 1f);
        powerUpRenderer.material.color = Color.Lerp(originalColor, Color.black, lerpFactor);

        // Update position for circular motion
        angle += moveSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);
    }
}

