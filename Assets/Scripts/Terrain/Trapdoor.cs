using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public bool open = false;
    void Start() {anim=GetComponent<Animator>();}
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            open=true;
            anim.SetBool("isOpen",true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            anim.SetBool("isOpen",false);
            open=false;
        }
    }
}
