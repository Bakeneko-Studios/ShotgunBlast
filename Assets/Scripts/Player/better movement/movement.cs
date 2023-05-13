using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public GameObject leftHand, rightHand;

    [Header("Player attributes")]
    public bool canMove = true;
    public bool grounded = true;
    public bool fatigued = false;
    public bool canSlide = true;
    public static float velocity;
    public static float nvvelocity;
    public static float yvelocity;
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
    public bool canJump;
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

    //idk what the c# absolute value function is so wrote one myself
    private float absolute_value(float n)
    {
        float result;
        if(n >= 0)
        {
            result = n;
        }
        else
        {
            result = -n;
        }
        return result;
    }
    private int absolute_value(int n)
    {
        int result;
        if (n >= 0)
        {
            result = n;
        }
        else
        {
            result = -n;
        }
        return result;
    }

    //angle calculator
    private float angleCalc(float x, float y)
    {
        float angle;
        if (x >= 0 && y >= 0)
        {
            angle = math.atan(y / x);
        }else if(x < 0 && y >= 0)
        {
            angle = 180f + math.atan(y / x);
        }else if(x >= 0 && y < 0)
        {
            angle = math.atan(y / x);
        }
        else
        {
            angle = (180f + math.atan(y / x)) - 360f;
        }
        return angle;
    }

    private void Awake()
    {
        instance=this;
        Player.reset();
    }
    void Start()
    {
        rb =GetComponent<Rigidbody>();
        yScale=transform.localScale.y;
        cam=Camera.main.transform;
        rightHand.transform.TransformDirection(1, 0, 0);
    }

    void Update()
    {
        
        //point the shit correctly
        
        //hashtag Bill trying to make the gun stop teleporting when looking at trapdoor but melting his brain in the process and ending up annotating all the code he wrote


        //F**KING RELATIVE ANGLES AND ABSOLUTE POINTS I AM NOT LOOKING AT THIS SH*T AGAIN ANYTIME SOON

        RaycastHit middleCast;
        //RaycastHit origCast;
        //float delta_x_angle;
        //float delta_y_angle;
        //RaycastHit newCast;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out middleCast))
        {
            //Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out origCast);
            //delta_x_angle = angleCalc(middleCast.point.z - origCast.point.z, middleCast.point.x - origCast.point.x);
            //delta_y_angle = angleCalc(middleCast.point.z - origCast.point.z, middleCast.point.y - origCast.point.y);
            if (true/*absolute_value(delta_x_angle) / 3f < 5f && absolute_value(delta_y_angle) / 3f < 5f*/)
            {
                rightHand.transform.LookAt(middleCast.point);
            }
            else
            {
                
                //Physics.Raycast(rightHand.transform.position, Vector3.forward, out newCast);
                //rightHand.transform.RotateAround(rightHand.transform.position, Vector3.up, delta_x_angle / 3f);
                //rightHand.transform.RotateAround(rightHand.transform.position, Vector3.forward, delta_y_angle / 3f);
                //rightHand.transform.LookAt(new Vector3(math.sin(angleCalc(origCast.point.z, origCast.point.y) - delta_y_angle / 3f), math.sin(angleCalc(origCast.point.z, origCast.point.x) - delta_x_angle / 3f), 0));

                //rightHand.transform.Rotate(delta_y_angle / 3f, delta_x_angle / 3f, 0f, Space.World);
                //Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out newCast);
                //rightHand.transform.LookAt(new Vector3(newCast.point.x, 0, 0));
                //rightHand.transform.LookAt(new Vector3(newCast.point.x, newCast.point.y, 0));
                //rightHand.transform.Rotate(new Vector3(0, math.sin(delta_x_angle / 3f), math.sin(delta_y_angle / 3f)));
            }
            //delta_rotation = new Vector3(math.sin(angleCalc(middleCast.point.z - origCast.point.z, middleCast.point.x - origCast.point.x)), math.sin(angleCalc(middleCast.point.z - origCast.point.z, middleCast.point.y - origCast.point.y)), 0);
            //rightHand.transform.LookAt(middleCast.point);
            //Debug.Log(middleCast.point);
        }



        vl = rb.velocity;
        velocity = vl.magnitude;
        nvvelocity = Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z);
        yvelocity = vl.y;
        bool wasGrounded = grounded;
        grounded = Physics.Raycast(transform.position,Vector3.down,height*0.5f+0.2f,ground,QueryTriggerInteraction.Ignore);
        Player.playerStatus = (grounded?Player.contact.ground:Player.contact.air);

        // if(!wasGrounded&&grounded)
        // {
        //     if(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)>1.5*(Mathf.Sqrt(vl.x*vl.x+vl.z*vl.z)) && vl.magnitude>10)
        //     {
        //         StartCoroutine(impactForce(Mathf.Log10(Mathf.Sqrt(vl.x*vl.x+vl.y*vl.y+vl.z*vl.z)/2)-0.5f));
        //     }
        //     //else StartCoroutine(jumpFatigue());
        // }

        h = canMove ? (Input.GetKey(UserSettings.keybinds["left"])?-1:0) + (Input.GetKey(UserSettings.keybinds["right"])?1:0) : 0;
        v = canMove ? (Input.GetKey(UserSettings.keybinds["back"])?-1:0) + (Input.GetKey(UserSettings.keybinds["forward"])?1:0) : 0;
        
        Vector3 vel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        if(vel.magnitude>speedCap)
        {
            Vector3 cvel = vel.normalized*speedCap;
            rb.velocity = new Vector3(cvel.x,rb.velocity.y,cvel.z);
        }

        if(grounded)
        {
            if(Input.GetKey(UserSettings.keybinds["crouch"]))
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
                    speed=crchSpeed*Player.speedMultiplier;
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
                speed=nrmSpeed*Player.speedMultiplier;
                rb.drag=drag;
            }
        }
        else
        {
            Player.state=Player.MoveState.air;
            rb.drag=0;
        }

        if(Input.GetKeyDown(UserSettings.keybinds["jump"])&&canMove&&canJump)
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
                    rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                    rb.AddForce(Vector3.up*10f,ForceMode.Impulse);
                }
            }
        }

        if(Input.GetKeyDown(UserSettings.keybinds["crouch"]))
        {
            transform.localScale = new Vector3(transform.localScale.x,crouchScale,transform.localScale.z);
            transform.Translate(Vector3.up*-(yScale-crouchScale));
            leftHand.transform.localScale = new Vector3(1,1/crouchScale,1);
            rightHand.transform.localScale = new Vector3(1,1/crouchScale,1);
        }
        if(Input.GetKeyUp(UserSettings.keybinds["crouch"]))
        {
            leftHand.transform.localScale = Vector3.one;
            rightHand.transform.localScale = Vector3.one;
            transform.Translate(Vector3.up*(yScale-crouchScale));
            transform.localScale = new Vector3(transform.localScale.x,yScale,transform.localScale.z);
        }
        //AutoJump when seeing stairs (wip buggy)
        if (true/*(Input.GetKeyDown(UserSettings.keybinds["forward"]) || Input.GetKeyDown(UserSettings.keybinds["backward"]) || Input.GetKeyDown(UserSettings.keybinds["left"]) || Input.GetKeyDown(UserSettings.keybinds["right"]))*/ && canMove)
        {
            if (Physics.Raycast(transform.position - new Vector3(0, 0.8f, 0), transform.TransformDirection(Vector3.forward), 2) && grounded && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), 6) && !Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 1)), 3))
            {
                rb.AddForce(transform.up * 1.5f, ForceMode.Impulse);
            }
        }
        //As you can see, in the if statement above, a bunch of stuff has been turned into annotations.
        //This is because I wanted to fix the bug that you hop around like a lunatic on the stairs, but I cant get the input stuff to work.
    }
    void FixedUpdate()
    {
        if(canMove)
        {
            if(grounded) direction = (player.forward*v+player.right*h).normalized;
            else direction = (player.forward*v).normalized*airControl*(v<=0?5:1)+(player.right*h).normalized*airControl*2;
            if(onSlope()&&Player.state!=Player.MoveState.air)
            {
                rb.AddForce(Vector3.ProjectOnPlane(direction,slopeCast.normal).normalized*speed*12.5f,ForceMode.Force);
                if(rb.velocity.y>0) rb.AddForce(Vector3.down*80f,ForceMode.Force);
            }
            else rb.AddForce(direction*speed*10f,ForceMode.Force);
            if(!onSlope()) rb.AddForce(Vector3.up*gravity,ForceMode.Force);
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
