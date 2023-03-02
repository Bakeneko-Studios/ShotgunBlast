using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Arm
{
    public float duration;
    [SerializeField] private GameObject[] blades;
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
    }
    IEnumerator punchDelay()
    {
        ArmManager.canPunch=false;
        yield return new WaitForSeconds(punchCD);
        ArmManager.canPunch=true;
    }
    void ability()
    {
        //
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
