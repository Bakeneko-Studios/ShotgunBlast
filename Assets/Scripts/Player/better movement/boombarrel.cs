using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boombarrel : MonoBehaviour
{
    public float fuse = 0;
    public float blastRadius;
    public float maxDmg;
    [Range(0,1)] public float scaling;
    public float forceMultiplier;
    public KeyCode key;

    private void OnTriggerEnter(Collider other)
    {if(other.tag=="projectile") StartCoroutine(boom());}
    IEnumerator boom()
    {
        yield return new WaitForSeconds(fuse);
        float dist = Vector3.Distance(this.transform.position,movement.instance.transform.position);
        if(dist<blastRadius)
        {
            float dmg = (scaling/((1/((1+scaling)*blastRadius*maxDmg))*(dist+scaling*blastRadius)))-scaling*maxDmg;
            playerHealth.instance.ChangeHealth(-dmg);
            movement.instance.rb.AddForce((movement.instance.transform.position-this.transform.position).normalized*dmg*forceMultiplier,ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}
