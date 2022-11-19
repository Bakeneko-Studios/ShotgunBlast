using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private Vector3 drag = new Vector3(10, 0, 10);
    private Vector3 verticalVelocity = Vector3.zero;
    private Vector2 horizontalInput;
    private bool isGrounded;
    private bool jump;
    //private bool canDash;
    //private bool dash;

   
    //Player Stats
    public float speed = 10f;
    public float jumpHeight = 3.5f;
    public float dashCoolDown = 4f;
    public float dashDistance = 5f;

    void Update()
    {
        //Grounded
        //isGrounded = Physics.CheckBox(transform.position, new Vector3(0.3f, 0.1f, 0.3f), Quaternion.identity, groundMask);
        //isGrounded = Physics.CheckSphere(transform.position, 0.4f, groundMask);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0f;
        }
        //Move Horisontally
        Vector3 horizontalVelocity = transform.right * horizontalInput.x + transform.forward * horizontalInput.y;
        controller.Move(horizontalVelocity * speed * Time.deltaTime);
        //Jump
        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }
        //Dash
        // if (dash)
        // {
        //     if (canDash)
        //     {
                
        //     }
        //     canDash = false;
        // }
        //Gravity
        verticalVelocity.y += gravity * Time.deltaTime;
        //Drag
        horizontalVelocity.x /= 1 + drag.x * Time.fixedDeltaTime;
        verticalVelocity.y /= 1 + drag.y * Time.fixedDeltaTime;
        horizontalVelocity.z /= 1 + drag.z * Time.fixedDeltaTime;

        controller.Move(verticalVelocity * Time.deltaTime);
    }


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
        //dash = true;
    }
    // public void OnStartDash()
    // {
    //     canDash = false;
    //     if (horizontalInput.x>=0 && horizontalInput.y==0)
    //     {
    //         //Dash in the direction facing (forward)
    //     }
    //     if (horizontalInput.x==0 && horizontalInput.y!=0)
    //     {
    //         //Dash Sideways
    //     }
    //     if (horizontalInput.x<0 && horizontalInput.y==0)
    //     {
    //         //Dash Backwards
    //     }
    //     if (horizontalInput.x!=0 && horizontalInput.y!=0)
    //     {
    //         //Dash Diagonally
    //     }
    // }
    // public void OnEndDash()
    // {
    //     //Initiate dash cooldown timer
    //     //if (touchedGroundOnce && coolDownOver)
    //     //  canDash = true;
    // }
}
