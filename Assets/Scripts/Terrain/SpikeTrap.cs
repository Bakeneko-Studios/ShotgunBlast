using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource sfx;
    public float damage,duration,slowPercent;
    public bool activated = false;
    void Start() {anim=GetComponent<Animator>();}
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            activated=true;
            anim.SetBool("isActivated",true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("isActivated",false);
            activated=false;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Player"))
        {
            movement.instance.rb.velocity=Vector3.zero;
            movement.instance.speedMultiplier-=slowPercent;
            movement.instance.canJump=false;
            InvokeRepeating("ouch",0,1);
        }
    }
    private void OnCollisionExit(Collision other) {
        if(other.collider.CompareTag("Player"))
        {
            movement.instance.canJump=true;
            movement.instance.speedMultiplier+=slowPercent;
            CancelInvoke();
        }
    }
    void ouch()
    {
        Debug.Log("boop");
        if(sfx!=null) sfx.Play();
        movement.instance.GetComponent<playerHealth>().ChangeHealth(-damage);
    }
}
