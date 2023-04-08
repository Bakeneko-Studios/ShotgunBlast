using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grapple : Arm
{
    public float range;
    public float force;
    public bool attached;
    [SerializeField] private Transform attachedObject;
    public GameObject grappleObject;
    GameObject grapple;
    Rigidbody rb;
    bool hasTarget;
    RaycastHit hit;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Image indicator;
    
    void Awake() {instance=this;}
    void Start()
    {
        grapple = GameObject.Instantiate(grappleObject);
        grapple.SetActive(false);
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
    }
    new void ability()
    {
        if(hasTarget)
        {
            grapple.SetActive(true);
            attached = true;
            attachedObject = hit.transform;
            movement.instance.gravity = 4.9f;
        }
        else attached = false;
    }
    void FixedUpdate()
    {
        hasTarget=false;
        indicator.gameObject.SetActive(false);
        if(Physics.Raycast(cam.position, cam.forward, out hit, range, mask))
        {
            hasTarget=true;
            indicator.gameObject.SetActive(true);
        }
        if(attached)
        {
            if (Input.GetKeyUp(UserSettings.keybinds["ability"]))
            {
                grapple.SetActive(false);
                attached = false;
                movement.instance.gravity = 19.6f;
            }
            Vector3 pos = (transform.position+attachedObject.position)/2;
            grapple.transform.position = pos;
            grapple.transform.rotation = Quaternion.LookRotation(attachedObject.position - transform.position);
            grapple.transform.localScale = new Vector3(Vector3.Distance(transform.position, attachedObject.transform.position),0,0);
            rb.AddForce(pos.normalized*force*Time.deltaTime);
        }
    }
}
