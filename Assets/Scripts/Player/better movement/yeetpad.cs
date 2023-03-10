using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeetpad : MonoBehaviour
{
    public float yeetForce;
    private bool yeet;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player") yeet=true;
    }
    void FixedUpdate()
    {
        if(yeet)
        {
            bounce();
            yeet=false;
        }
    }
    void bounce()
    {
        movement.instance.rb.drag=0;
        movement.instance.rb.AddForce(transform.up*yeetForce,ForceMode.Impulse);
    }
}
