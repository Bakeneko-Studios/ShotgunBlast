using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementBill : MonoBehaviour
{
    //Just Variables used
    private Vector2 horizontalInput;
    private bool jump;
    //TODO: Dash
    private bool dash;
    //float startTime;
    Rigidbody p_rigidbody;
    bool collisionState = false;
    float secondsVar;
    float secondsSinceJump;
    float currentMaxSpeed;

    //Stats
    public float jumpCoolDown = 0.5f;
    public float jumpForce = 10f;
    public float maxSpeedG=10f;//max speed on ground
    public float maxSpeedA=2f;//max speed in air
    void Start()
    {
        //startTime = Time.time;
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
        if (jump)
        {
            //test if player jumped more than 0.5 seconds ago
            if (secondsVar - secondsSinceJump > jumpCoolDown)
            {
                //jump with speed of 10
                p_rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
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
            currentMaxSpeed = maxSpeedG;
        }
        else
        {
            currentMaxSpeed = maxSpeedA;
        }
        //Move with respective speed if not over max speed
        if (p_rigidbody.velocity.x < currentMaxSpeed && p_rigidbody.velocity.z < currentMaxSpeed)
        {
            p_rigidbody.AddRelativeForce(new Vector3(horizontalInput.x, 0, horizontalInput.y), ForceMode.VelocityChange);
        }
    }

    //Input Stuff
    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }
    public void OnJumpPressed()
    {
        jump = true;
    }
    public void OnDashPressed()
    {
        dash = true;
    }
}
