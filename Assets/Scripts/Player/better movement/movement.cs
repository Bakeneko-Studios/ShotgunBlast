using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movement : MonoBehaviour
{
    [Header("Components")]
    public Transform player;
    public Rigidbody rb;
    public LayerMask ground;

    [Header("Player attributes")]
    public bool canMove = true;
    public bool grounded;
    public bool jumping;
    public static float velocity;
    public static float nvvelocity;
    private float speed;
    float h;
    float v;
    float t;
    Vector3 vl;
    Vector3 direction;
    RaycastHit slopeCast;

    [Header("Values")]
    public float nrmSpeed;
    public float sprSpeed;
    public float crchSpeed;
    public float speedCap;
    public float jumpForce;
    public float drag;
    public float idleDrag;
    public float airFriction;
    public float height;
    public float crouchScale;
    private float yScale;
    public float maxSlopeAngle;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        yScale = transform.localScale.y;
    }

    void Update()
    {
        vl = rb.velocity;
        velocity = vl.magnitude;
        nvvelocity = Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z);
        bool wasGrounded = grounded;
        grounded = Physics.Raycast(transform.position,Vector3.down,height*0.5f+0.2f,ground);
        Player.playerStatus = (grounded?Player.contact.ground:Player.contact.air);

        if(!wasGrounded&&grounded)
        {
            jumping=false;
            if(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)>1.5*(Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z)) && vl.magnitude>10)
            {
                StartCoroutine(impactForce(Mathf.Log10(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)/2)-0.5f));
            }
        }

        h = canMove ? Input.GetAxis("Horizontal") : 0;
        v = canMove ? Input.GetAxis("Vertical") : 0;
        
        Vector3 vel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        if(vel.magnitude>speedCap)
        {
            Vector3 cvel = vel.normalized*speedCap;
            rb.velocity = new Vector3(cvel.x,rb.velocity.y,cvel.z);
        }

        if(grounded&&Input.GetKey(crouchKey))
        {
            Player.state=Player.MoveState.crouch;
            speed=crchSpeed;
            rb.drag=drag;
        }
        else if(grounded&&h==0&&v==0)
        {
            Player.state=Player.MoveState.idle;
            rb.drag=drag;
        }
        else if(grounded&&Input.GetKey(sprintKey)&&v>0)
        {
            Player.state=Player.MoveState.sprint;
            speed=sprSpeed;
            rb.drag=drag;
        }
        else if(grounded)
        {
            Player.state=Player.MoveState.walk;
            speed=nrmSpeed;
            rb.drag=drag;
        }
        else
        {
            Player.state=Player.MoveState.air;
            rb.drag=0;
        }

        if(Input.GetKeyDown(jumpKey)&&grounded&&canMove)
        {
            jumping=true;
            rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
            rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
        }

        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x,crouchScale,transform.localScale.z);
            transform.position = new Vector3(transform.position.x,transform.position.y-(yScale-crouchScale),transform.position.z);
        }
        if(Input.GetKeyUp(crouchKey))
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+(yScale-crouchScale),transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x,yScale,transform.localScale.z);
        }
    }
    void FixedUpdate()
    {
        if(canMove)
        {
            direction = player.forward*v+player.right*h;
            if(onSlope()&&!jumping)
            {
                rb.AddForce(Vector3.ProjectOnPlane(direction,slopeCast.normal).normalized*speed*15f,ForceMode.Force);
                if(rb.velocity.y>0) rb.AddForce(Vector3.down*80f,ForceMode.Force);
            }
            else if(grounded) rb.AddForce(direction.normalized*speed*10f,ForceMode.Force);
            else rb.AddForce(direction.normalized*speed*10f*airFriction,ForceMode.Force);
            rb.useGravity=!onSlope();
        }
    }
    IEnumerator impactForce(float time)
    {
        canMove=false;
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        canMove=true;
    }

    private bool onSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out slopeCast,height*0.5f+0.3f))
        {
            float angle = Vector3.Angle(Vector3.up,slopeCast.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.tag=="ground") {grounded=true;}
    //     if(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)>1.5*(Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z)) && vl.magnitude>10)
    //     {
    //         StartCoroutine(impactForce(Mathf.Log10(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)/2)-0.5f));
    //     }
    // }
    // private void OnCollisionStay(Collision other) {
    //     if(other.gameObject.tag=="ground") {grounded=true;}
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     if(other.gameObject.tag=="ground") {grounded=false;}
    // }
}
