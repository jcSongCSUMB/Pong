using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float speed;  
    private Rigidbody rb;
    public ScoreManager scoreManager;
    public int player1Wins = -1;
    public CameraShake cameraShake;

    public AudioSource hitPlayerSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();

        // Find and assign the CameraShake script
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogError("CameraShake script not found on the main camera.");
        }
    }
    
    void ResetBallPosition()
    {
        transform.position = Vector3.zero;
        LaunchBall();
    }

    void LaunchBall()
    {
        Vector3 launchDirection = new Vector3(player1Wins, 0, 0).normalized;
        speed = 10.0f;
        rb.velocity = launchDirection * speed;
    }

    void Bounce(Collision collision)
    {
        // Calculate hit factor
        float hitFactor = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;

        // Calculate angle
        float angle = Mathf.Lerp(-90, 90, (hitFactor + 1) / 2); // Convert hitFactor range from [-1, 1] to [0, 1]

        Vector3 direction;
        if (collision.transform.position.x < 0)
        {
            direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        }
        else
        {
            direction = Quaternion.Euler(0, 0, -angle) * Vector3.left;
        }
        
        rb.velocity = direction.normalized * speed;
        
        Renderer paddleRenderer = collision.gameObject.GetComponent<Renderer>();
        if (paddleRenderer != null)
        {
            paddleRenderer.material.color = GetRandomBrightColor();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bounce(collision);
            hitPlayerSource.Play();
            // Trigger the camera shake effect
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake(0.1f, 0.2f));  // Duration and magnitude of the shake
            }
        }
        else if (collision.gameObject.CompareTag("PlayerBase"))
        {
            if (collision.gameObject.name == "Player_1_Base")
            {
                player1Wins = 1;
                scoreManager.Player2Score(); 
            }
            else if (collision.gameObject.name == "Player_2_Base")
            {
                player1Wins = -1;
                scoreManager.Player1Score();
            }

            ResetBallPosition();
        }
    }
    
    Color GetRandomBrightColor()
    {
        float hue = Random.Range(0f, 1f);  // Hue from 0 to 1
        float saturation = Random.Range(0.7f, 1f);  // Saturation from 0.7 to 1 for bright colors
        float value = Random.Range(0.7f, 1f);  // Value (brightness) from 0.7 to 1

        return Color.HSVToRGB(hue, saturation, value);
    }
}

