using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public float minSpeed = 5f;
    public float speedMultiplier = 0.75f;

    public AudioSource hitSlowDown;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            
            hitSlowDown.Play();
            
            BallBehavior ball = other.gameObject.GetComponent<BallBehavior>();
            if (ball != null)
            {
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    ball.speed = Mathf.Max(ball.speed * speedMultiplier, minSpeed);
                    ballRb.velocity = ballRb.velocity.normalized * ball.speed;
                    Debug.Log($"Ball Slows Down! {ball.speed}");
                }
            }
        }
    }
}
