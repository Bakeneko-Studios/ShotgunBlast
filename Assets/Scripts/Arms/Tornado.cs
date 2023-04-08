using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Arm
{
    public float duration;
    [SerializeField] private GameObject[] blades;
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
