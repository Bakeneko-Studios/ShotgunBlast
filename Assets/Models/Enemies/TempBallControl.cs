using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBallControl : MonoBehaviour
{
    public Animator anim;
    void Start() 
    {
        anim = this.GetComponent<Animator>();    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Play("BallAttack");
        } 
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.Play("BallDeath");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.Play("BallDeploy");
        }
    }
}
