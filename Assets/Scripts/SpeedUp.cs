using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float speedMultiplier = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BallBehavior ball = other.gameObject.GetComponent<BallBehavior>();
            if (ball != null)
            {
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    ball.speed = Mathf.Min(ball.speed * speedMultiplier, maxSpeed);
                    ballRb.velocity = ballRb.velocity.normalized * ball.speed;
                    Debug.Log($"Ball Speed Up! {ball.speed}");
                }
            }
        }
    }
}
