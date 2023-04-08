using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : Arm
{
    public float radius;
    public float duration;
    [SerializeField] private Transform shield;
    void Awake() {instance=this;}
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
        shield.localScale = Vector3.one*radius;
    }
    new void ability()
    {
        //
    }
}
