using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grapple : Arm
{
    public float range;
    public float speed;
    public bool attached;
    public GameObject grappleObject;
    GameObject grapple;
    bool hasTarget;
    RaycastHit hit;
    [SerializeField] private Image indicator;
    
    void Awake() {instance=this;}
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
    }
    void ability()
    {
        if(hasTarget)
        {
            grapple = GameObject.Instantiate(grappleObject);
            grapple.transform.position = transform.position;
            grapple.AddComponent<Rigidbody>();
            grapple.GetComponent<Rigidbody>().velocity = (hit.point - transform.position).normalized * speed;
            attached = true;
        }
        else attached = false;
    }
    void Update()
    {
        hasTarget=false;
        indicator.gameObject.SetActive(false);
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, range))
        {
            if(hit.collider.CompareTag("enemy"))
            {
                hasTarget=true;
                indicator.gameObject.SetActive(true);
            }
        }
        if(attached)
        {
            GetComponent<Rigidbody>().velocity = grapple.GetComponent<Rigidbody>().velocity;
            if (Input.GetKeyUp(UserSettings.keybinds["ability"]))
            {
                Destroy(grapple);
                attached = false;
            }
        }
    }
    IEnumerator punchDelay()
    {
        ArmManager.canPunch=false;
        yield return new WaitForSeconds(punchCD);
        ArmManager.canPunch=true;
    }
    IEnumerator abilityDelay()
    {
        ArmManager.isBusy=true;
        canMorb=false;
        yield return new WaitForSeconds(abilityCD);
        canMorb=true;
        ArmManager.isBusy=false;
    }
}
