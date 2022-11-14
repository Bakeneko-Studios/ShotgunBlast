using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class move : MonoBehaviour
{
    //initialization
    float startTime = Time.time;
    Rigidbody p_rigidbody;
    bool collisionState = false;
    public float secondsVar;
    public float secondsSinceJump;
    void Start()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        secondsSinceJump = Time.time;
    }
    //collision state detection
    private void OnCollisionEnter(Collision collision)
    {
        collisionState = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        collisionState = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        //test if spacebar is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            //test if player jumped more than 0.5 seconds ago
            if (secondsVar - secondsSinceJump > 0.5f)
            {
                //jump with speed of 10
                p_rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.VelocityChange);
                //reset the time since player jumped
                secondsSinceJump = Time.time;
            }
        }
        //bugfix of getting stuck
        collisionState = true;
    }
    private void FixedUpdate()
    {
        //get current time
        secondsVar = Time.time;
        //test for collision state
        if (collisionState)
        {
            //movement in 4 directions with max speed of 10 (squared to 100) when touching the ground
            if (Input.GetKey(KeyCode.W))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(0, 0, 1), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(-1, 0, 0), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(0, 0, -1), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(100 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(100 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(1, 0, 0), ForceMode.VelocityChange);
                }
            }
        }
        else
        {
            //movement in 4 directions with max speed of 2 (squared to 4) when not touching the ground
            if (Input.GetKey(KeyCode.W))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(0, 0, 1), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(-1, 0, 0), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(0, 0, -1), ForceMode.VelocityChange);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (p_rigidbody.velocity.z < Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x > -Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z) && p_rigidbody.velocity.z > -Mathf.Sqrt(4 - p_rigidbody.velocity.x * p_rigidbody.velocity.x) && p_rigidbody.velocity.x < Mathf.Sqrt(4 - p_rigidbody.velocity.z * p_rigidbody.velocity.z))
                {
                    p_rigidbody.AddRelativeForce(new Vector3(1, 0, 0), ForceMode.VelocityChange);
                }
            }
        }
        //debug
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -1, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    void Update()
    {

    }
}
