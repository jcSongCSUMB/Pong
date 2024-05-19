using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPaddle : MonoBehaviour
{
    public float unitsPerSecond = 10.0f;
    public string inputAxis;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float verticalValue = Input.GetAxis(inputAxis);

        if (verticalValue != 0)
        {
            Vector3 newPosition = transform.position + Vector3.up * verticalValue * unitsPerSecond * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}

