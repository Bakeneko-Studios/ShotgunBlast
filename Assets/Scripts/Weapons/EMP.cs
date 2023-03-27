using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : Shotgun
{
    public float fireRate = 2f;
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
    }

    new public void FireGun()
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
        StartCoroutine(shootDelay());
    }

    IEnumerator shootDelay()
    {
        yield return new WaitForSeconds(fireRate/Player.firerateMultiplier);
        canShoot=true;
    }

    void Update() 
    {
        if (Input.GetKeyDown(UserSettings.keybinds["attack"]))
            FireGun();
    }   
}