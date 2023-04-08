using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinforcedArm : Arm
{
    public float yeetForce;
    void Awake() {instance=this;}
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
    }
    new void ability()
    {
        StartCoroutine(abilityDelay());
        RaycastHit hit;
        if(Physics.Raycast(cam.position,Vector3.forward,out hit,reach))
        {
            if(hit.collider.tag=="enemy")
            {
                hit.collider.GetComponent<enemyFramework>().health-=abilityDamage;
                hit.rigidbody.AddForce(Vector3.up*yeetForce);
            }
        }
        if(cam.GetComponent<cameraShake>()!=null)
            StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration,cameraShakeMagnitude));
    }
}
