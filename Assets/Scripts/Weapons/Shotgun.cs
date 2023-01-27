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
    //Effects
    public GameObject pellet;
    //TODO public GameObject muzzleFire;
    //Other Stuff
    public Transform bulletExit;
    protected Transform cam;
    protected Animation anim;
    public List<Quaternion> pelletAngles;
    [HideInInspector]public bool isAnimate;
    //Variables
    public bool canShoot;
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
        canShoot = true;
    }
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anim = GetComponentInChildren<Animation>();
        if (anim != null) {
            isAnimate = true;
        }
    }

    public void FireGun()
    {
        if (canShoot && Time.timeScale>0)
        {
            if(cam.GetComponent<cameraShake>()!=null)
                StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration, cameraShakeMagnitude));
            for (int i=0; i<pelletCount; i++)
            {
                pelletAngles[i] = Random.rotation;
                GameObject pShot = Instantiate(pellet, bulletExit.position, cam.rotation);

                pShot.GetComponent<Pellet>().playersBullet = true; // avoid friendly fire from enemies

                pShot.transform.rotation = Quaternion.RotateTowards(pShot.transform.rotation, pelletAngles[i], spreadAngle);
                pShot.GetComponent<Rigidbody>().AddForce(pShot.transform.forward * pelletSpeed);
            }
            canShoot=false;
            if (isAnimate) {
                anim.Play();
                StartCoroutine(Reload());
            } else {
                canShoot = true;
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    void Update() 
    {
        if (Input.GetKeyDown(UserSettings.keybinds["attack"]))
            FireGun();
    }   
}