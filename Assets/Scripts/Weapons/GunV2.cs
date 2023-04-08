using System.Collections;
using UnityEngine;

public abstract class GunV2 : MonoBehaviour
{
    public float damage;
    public Vector3 spreadAngle;
    public float reloadTime;
    public float fireRate;
    public int ammoCapacity;
    protected int ammoInClip;
    public Transform bulletExit;
    protected Transform cam;
    [SerializeField] protected Animator anim;
    [HideInInspector] public bool isAnimate;
    public bool canShoot;
    [SerializeField] protected LayerMask mask;
    [SerializeField] protected ParticleSystem muzzleFlash, impactMark;
    [SerializeField] protected TrailRenderer bulletTrail;

    protected virtual void Start()
    {
        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
        isAnimate = anim!=null;
        ammoInClip = ammoCapacity;
    }
    public abstract void FireGun();
    protected IEnumerator Reload()
    {
        if(isAnimate && !ArmManager.isBusy)
            anim.SetTrigger("spin");
        else if(isAnimate)
            anim.SetTrigger("spin");
        ArmManager.isBusy=true;
        canShoot=false;
        yield return new WaitForSeconds(reloadTime/Player.firerateMultiplier);
        ammoInClip = ammoCapacity;
        canShoot=true;
        ArmManager.isBusy=false;
    }
    protected IEnumerator shotDelay()
    {
        canShoot=false;
        yield return new WaitForSeconds(fireRate/Player.firerateMultiplier);
        canShoot=true;
    }
    protected IEnumerator BulletTravel(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 pos = trail.transform.position;

        while(time<1)
        {
            trail.transform.position = Vector3.Lerp(pos, hit.point, time);
            time += Time.deltaTime/trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
        Instantiate(impactMark, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }
}
