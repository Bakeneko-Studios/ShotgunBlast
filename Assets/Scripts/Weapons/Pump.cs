using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : Shotgun
{
    //Gun Stats
    public int clipSize = 6;
    public int ammoInClip = 2;
    public float fireRate = 1f;

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
        isAnimate = anim!=null && animOneHand!=null;
    }

    new public void FireGun()
    {
        if(ammoInClip==0)
        {
            canShoot=false;
            if(isAnimate && !ArmManager.isBusy)
                anim.Play();
            else if(isAnimate)
                animOneHand.Play();
            StartCoroutine(Reload());
        }
        else
        {
            canShoot=false;
            anim.Stop();
            StopAllCoroutines();
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
            StartCoroutine(shotDelay());
        }
    }

    IEnumerator shotDelay()
    {
        yield return new WaitForSeconds(fireRate/Player.firerateMultiplier);
        canShoot=true;
    }

    IEnumerator Reload()
    {
        ArmManager.isBusy=true;
        while(ammoInClip<clipSize)
        {
            yield return new WaitForSeconds(reloadTime/Player.firerateMultiplier);
            ammoInClip++;
            canShoot=true;
        }
        ArmManager.isBusy=false;
    }

    void Update() 
    {
        if (Input.GetKeyDown(UserSettings.keybinds["attack"]) && canShoot && Time.timeScale>0)
            FireGun();
        else if(Input.GetKeyDown(UserSettings.keybinds["reload"]))
            StartCoroutine(Reload());
    }   
}