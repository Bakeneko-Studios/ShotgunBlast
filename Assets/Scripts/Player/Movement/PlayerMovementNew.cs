using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public Transform orientation;
    [SerializeField] private LayerMask groundMask;
    public float groundDrag;

    private Vector2 horizontalInput;
    private bool isGrounded;
    private bool jump;
    private bool canDash;
    private bool dash;
    //Player Stats
    public float speed = 10f;
    public float jumpForce = 12f;
    public float airMultiplier = 0.4f;
    public float dashCoolDown = 4f;
    public float dashDistance = 5f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        rb.AddForce(Vector3.down*10f*Time.deltaTime);
        //Grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundMask);
        MovePlayer();
        SpeedControl();
        if (jump)
        {
            if (isGrounded)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);                
            }
            jump = false;
        }
        //Drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * horizontalInput.y + orientation.right * horizontalInput.x;

        if (isGrounded)
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * speed * airMultiplier, ForceMode.Force);

    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = limitedVel;
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
