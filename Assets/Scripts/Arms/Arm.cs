using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //general
    public int damage;
    public float punchCD;
    public float reach;
    [HideInInspector] public bool isAnimate;
    protected Animation anim;
    protected Transform cam;

    //ability
    public bool canMorb;
    public float abilityCD;
    public float abilityDamage;
    [SerializeField] protected Animation abilityAnim;
    public float cameraShakeDuration;
    public float cameraShakeMagnitude;
    void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animation>();
        isAnimate = !(anim==null);
    }
    public void punch()
    {
        if(isAnimate) anim.Play();
        StartCoroutine(punchDelay());
        RaycastHit hit;
        if(Physics.Raycast(cam.position,Vector3.forward,out hit,reach))
        {
            if(hit.collider.tag=="enemy")
            {
                hit.collider.GetComponent<enemyFramework>().ChangeHealth(-damage);
                Debug.Log("boop");
            }
        }
    }
    public void useAbility()
    {ability();}
    void ability()
    {}
    IEnumerator punchDelay()
    {
        ArmManager.canPunch=false;
        yield return new WaitForSeconds(punchCD);
        ArmManager.canPunch=true;
    }
}
