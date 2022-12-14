using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movement : MonoBehaviour
{
    public static movement instance;
    [Header("Components")]
    public Transform player;
    public Rigidbody rb;
    public LayerMask ground;
    public Transform cam;

    [Header("Player attributes")]
    public bool canMove = true;
    public bool grounded = true;
    public bool fatigued = false;
    public bool canSlide = true;
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
    public float gravity;
    public float nrmSpeed;
    public float sprSpeed;
    public float crchSpeed;
    public float speedCap;
    public float jumpForce;
    public float jumpDelay;
    public float drag;
    public float airControl;
    public float slideThreshold;
    public float slideForce;
    public float slideCooldown;
    public float wallbounceForce;
    public float height;
    public float crouchScale;
    private float yScale;
    public float maxSlopeAngle;
    
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    private void Awake() {instance=this;}
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        yScale=transform.localScale.y;
        cam=Camera.main.transform;
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
            if(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)>1.5*(Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z)) && vl.magnitude>10)
            {
                StartCoroutine(impactForce(Mathf.Log10(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)/2)-0.5f));
            }
            //else StartCoroutine(jumpFatigue());
        }

        h = canMove ? (Input.GetKey(KeyCode.A)?-1:0) + (Input.GetKey(KeyCode.D)?1:0) : 0;
        v = canMove ? (Input.GetKey(KeyCode.S)?-1:0) + (Input.GetKey(KeyCode.W)?1:0) : 0;
        
        Vector3 vel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        if(vel.magnitude>speedCap)
        {
            Vector3 cvel = vel.normalized*speedCap;
            rb.velocity = new Vector3(cvel.x,rb.velocity.y,cvel.z);
        }

        if(grounded)
        {
            if(Input.GetKey(crouchKey))
            {
                if(nvvelocity>slideThreshold)
                {
                    if(Player.state!=Player.MoveState.slide&&canSlide)
                    {
                        rb.AddForce(rb.velocity.normalized*slideForce,ForceMode.Impulse);
                        StartCoroutine(slideCD());
                    }
                    Player.state=Player.MoveState.slide;
                    //fatigued=true;
                    h=0;v=0;
                    rb.drag=1;
                }
                else
                {
                    Player.state=Player.MoveState.crouch;
                    //fatigued=false;
                    speed=crchSpeed;
                    rb.drag=drag;
                }
            }
            else if(h==0&&v==0)
            {
                Player.state=Player.MoveState.idle;
                rb.drag=drag;
            }
            // else if(Input.GetKey(sprintKey)&&v>0)
            // {
            //     Player.state=Player.MoveState.sprint;
            //     speed=sprSpeed;
            //     rb.drag=drag;
            // }
            else
            {
                Player.state=Player.MoveState.walk;
                speed=nrmSpeed;
                rb.drag=drag;
            }
        }
        else
        {
            Player.state=Player.MoveState.air;
            rb.drag=0;
        }

        if(Input.GetKeyDown(jumpKey)&&canMove)
        {
            if(grounded)
            {
                if(Player.state!=Player.MoveState.slide)
                {
                    rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
                    rb.AddForce(transform.up*(fatigued?jumpForce/2:jumpForce),ForceMode.Impulse);
                }
            }
            else
            {
                RaycastHit hit;
                if(Physics.Raycast(cam.position,cam.forward,out hit,0.8f))
                {
                    rb.AddForce(hit.normal*wallbounceForce,ForceMode.Impulse);
                    rb.AddForce(Vector3.up*12f,ForceMode.Impulse);
                }
            }
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
            if(onSlope()&&Player.state!=Player.MoveState.air)
            {
                rb.AddForce(Vector3.ProjectOnPlane(direction,slopeCast.normal).normalized*speed*12.5f,ForceMode.Force);
                if(rb.velocity.y>0) rb.AddForce(Vector3.down*80f,ForceMode.Force);
            }
            else if(grounded) rb.AddForce(direction.normalized*speed*10f,ForceMode.Force);
            else rb.AddForce(direction.normalized*speed*10f*airControl,ForceMode.Force);
            rb.AddForce(0,onSlope()?0:gravity,0,ForceMode.Force);
        }
    }
    IEnumerator impactForce(float time)
    {
        canMove=false;
        yield return new WaitForSeconds(time);
        canMove=true;
    }
    IEnumerator jumpFatigue()
    {
        fatigued=true;
        yield return new WaitForSeconds(jumpDelay);
        fatigued=false;
    }
    IEnumerator slideCD()
    {
        canSlide=false;
        yield return new WaitForSeconds(slideCooldown);
        canSlide=true;
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
