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
    public string theAnimation;
    //Effects
    public GameObject pellet;
    //TODO public GameObject muzzleFire;
    //Other Stuff
    public Transform bulletExit;
    private Transform cam;
    private Animator animator;
    public List<Quaternion> pelletAngles;
    //Variables
    public bool isAnimate;
    private bool canShoot;
    private WaitForSeconds reloadWait;
    //camera shake
    public float cameraShakeDuration = 0.3f;
    public float cameraShakeMagnitude = 0.6f;

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
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        animator = this.GetComponent<Animator>();
    }

    public void FireGun()
    {
        if (canShoot)
        {
            if(cam.GetComponent<cameraShake>()!=null) StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration, cameraShakeMagnitude));
            for (int i=0; i<pelletCount; i++)
            {
                pelletAngles[i] = Random.rotation;
                GameObject pShot = Instantiate(pellet, bulletExit.position, cam.rotation);
                // [!] from this exclamation mark all the way to the next exclamation mark, code is added by Bill. Delete if no want.
                //      code that changes initial position of the pellets to the tip of the shotgun barrel. Direction is not changed. Shooting seems normal. delete if no want.
                pShot.transform.Translate(new Vector3(0.2f,-0.2f,0.7f));
                pShot.GetComponent<Pellet>().playersBullet = true; // avoid friendly fire from enemies
                // [!] code above is code edited by Bill. Delete if no want.
                pShot.transform.rotation = Quaternion.RotateTowards(pShot.transform.rotation, pelletAngles[i], spreadAngle);
                pShot.GetComponent<Rigidbody>().AddForce(pShot.transform.forward * pelletSpeed);
                
            }
            canShoot=false;
            if (isAnimate)
            {
                animator.Play(theAnimation);
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
