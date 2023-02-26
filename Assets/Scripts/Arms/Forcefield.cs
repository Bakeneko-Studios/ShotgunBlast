using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : Arm
{
    public float radius;
    public float duration;
    [SerializeField] private Transform shield;
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
        shield.localScale = Vector3.one*radius;
    }
    void ability()
    {
        //
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
