using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Arm
{
    public int ammo;
    public float duration;
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
        //
    }
}
