using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadImpalement : Shotgun
{
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
            if(isAnimate && !ArmManager.isBusy)
                anim.Play();
            else if(isAnimate)
                animOneHand.Play();
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        ArmManager.isBusy=true;
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        ArmManager.isBusy=false;
    }

    void Update() 
    {
        if (Input.GetKeyDown(UserSettings.keybinds["attack"]))
            FireGun();
    }   
}