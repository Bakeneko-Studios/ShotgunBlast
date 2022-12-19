using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yeetpad : MonoBehaviour
{
    public float yeetForce;
    private void OnTriggerEnter(Collider other)
    {if(other.tag=="Player") movement.instance.rb.AddForce(transform.up*yeetForce,ForceMode.Impulse);}
}
