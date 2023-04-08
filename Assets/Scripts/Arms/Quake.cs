using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake : Arm
{
    public float quakeRadius;
    [SerializeField] private Transform abilityHitbox;
    private List<GameObject> targetsInRange = new List<GameObject>();
    void Awake() {instance=this;}
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
        canMorb = false;
        StartCoroutine(abilityDelay());
        abilityHitbox.localScale = new Vector3(quakeRadius,1,quakeRadius);
    }
    new void ability()
    {
        slam();
    }
    IEnumerator slam()
    {
        StartCoroutine(abilityDelay());
        movement.instance.rb.AddForce(Vector3.down*2000);
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject g in targetsInRange)
        {
            g.GetComponent<enemyFramework>().health-=abilityDamage;
        }
        if(cam.GetComponent<cameraShake>()!=null)
            StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration,cameraShakeMagnitude));
    }
    private void OnTriggerEnter(Collider other)
    {if(other.CompareTag("enemy")) targetsInRange.Add(other.gameObject);}
    private void OnTriggerExit(Collider other) 
    {if(other.CompareTag("enemy")) targetsInRange.Remove(other.gameObject);}
}
