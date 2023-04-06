using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public float spreadAngle;
    public float pelletSpeed;
    public float reloadTime;
    public float fireRate;
    public int ammoCapacity;
    protected int ammoInClip;
    public GameObject pellet;
    public Transform bulletExit;
    protected Transform cam;
    [SerializeField] protected Animation anim;
    [SerializeField] protected Animation animOneHand;
    [HideInInspector]public bool isAnimate;
    public bool canShoot;

    public abstract void FireGun();
    protected IEnumerator Reload()
    {
        ArmManager.isBusy=true;
        canShoot=false;
        yield return new WaitForSeconds(reloadTime/Player.firerateMultiplier);
        canShoot=true;
        ArmManager.isBusy=false;
    }
    protected IEnumerator shotDelay()
    {
        canShoot=false;
        yield return new WaitForSeconds(fireRate/Player.firerateMultiplier);
        canShoot=true;
    }

}
