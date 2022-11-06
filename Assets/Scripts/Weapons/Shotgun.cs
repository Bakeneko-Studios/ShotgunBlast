using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    //Gun Stats
    public int pelletCount;
    public float spreadAngle;
    public float pelletSpeed;
    public float reloadTime;
    public bool isAnimate;
    //Effects
    public float exitDistance = 1f;
    public GameObject pellet;
    public GameObject muzzleFire;
    //Other Stuff
    public Transform bulletExit;
    public Transform cam;
    public Animator animator;
    public List<Quaternion> pelletAngles;
    //Variables
    private bool canShoot;
    private WaitForSeconds reloadWait;
    void Awake()
    {
        pelletAngles = new List<Quaternion>();
        for (int i = 0; i<pelletCount; i++)
        {
            pelletAngles.Add(Quaternion.Euler(Vector3.zero));
        }
        reloadWait = new WaitForSeconds(reloadTime);
        canShoot = true;
    }
    void Update()
    {

    }

    public void FireGun()
    {
        if (canShoot)
        {
            for (int i=0; i<pelletCount; i++)
            {
                pelletAngles[i] = Random.rotation;
                GameObject pShot = Instantiate(pellet, bulletExit.position, cam.rotation);
                pShot.transform.rotation = Quaternion.RotateTowards(pShot.transform.rotation, pelletAngles[i], spreadAngle);
                pShot.GetComponent<Rigidbody>().AddForce(pShot.transform.forward * pelletSpeed);
            }
            canShoot=false;
            if (isAnimate)
            {
                animator.Play("GunSpin");
                StartCoroutine(Reload());
            }
            else
            {
                canShoot = true;
            }
        }
            
    }
    IEnumerator Reload()
    {
        yield return reloadWait;
        canShoot = true;
    }
    public void OnPrimaryFire()
    {
        FireGun();
    }
}
