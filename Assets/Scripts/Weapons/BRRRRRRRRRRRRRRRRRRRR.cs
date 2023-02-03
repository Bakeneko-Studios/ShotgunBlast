using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRRRRRRRRRRRRRRRRRRRR : Shotgun
{
    //Gun Stats
    public int ammoInClip = 69420;
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
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animation>();
        isAnimate = !(anim==null);
    }

    new public void FireGun()
    {
        if(ammoInClip==0)
        {
            canShoot=false;
            if (isAnimate)
            {
                anim.Play();
                StartCoroutine(Reload());
            }
            else canShoot = true;
        }
        else
        {
            canShoot=false;
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
            ammoInClip--;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammoInClip = 69420;
        canShoot = true;
    }

    void Update() 
    {
        if (Input.GetKey(UserSettings.keybinds["attack"]) && canShoot && Time.timeScale>0)
            FireGun();
        else if(Input.GetKeyDown(UserSettings.keybinds["reload"]))
            StartCoroutine(Reload());
    }   
}
