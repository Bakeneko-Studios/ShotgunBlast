using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPArm : Arm
{
    public float EMPRadius;
    [SerializeField] private Transform abilityHitbox;
    private List<GameObject> targetsInRange = new List<GameObject>();
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
        abilityHitbox.localScale = Vector3.one*EMPRadius;
    }
    void Awake() {instance=this;}
    new void ability()
    {
        StartCoroutine(abilityDelay());
        foreach (GameObject g in targetsInRange)
        {
            g.GetComponent<enemyFramework>().ChangeEMPHealth(-abilityDamage);
        }
        if(cam.GetComponent<cameraShake>()!=null)
            StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration,cameraShakeMagnitude));
    }
    private void OnTriggerEnter(Collider other)
    {if(other.CompareTag("enemy")) targetsInRange.Add(other.gameObject);}
    private void OnTriggerExit(Collider other) 
    {if(other.CompareTag("enemy")) targetsInRange.Remove(other.gameObject);}
}
