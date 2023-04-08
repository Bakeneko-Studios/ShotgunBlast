using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_StandardV2 : ShotgunV2
{
    protected override void Start()
    {
        base.Start();
        raycastAngles = new List<Vector3>();
        for (int i = raycastAngles.Count; i < raycastCount; i++)
            raycastAngles.Add(RandomDirection());
        canShoot = true;
    }

    public override void FireGun()
    {
        ammoInClip--;
        if(cam.GetComponent<cameraShake>()!=null)
            StartCoroutine(cam.GetComponent<cameraShake>().shakeCamera(cameraShakeDuration, cameraShakeMagnitude));
        muzzleFlash.Play();
        for (int i = 0; i < raycastCount; i++)
        {
            Physics.Raycast(bulletExit.position,cam.transform.forward+raycastAngles[i], out RaycastHit hit, float.PositiveInfinity);
            if(hit.collider.TryGetComponent<enemyFramework>(out enemyFramework ef))
                ef.ChangeHealth(-damage*Player.damageMultiplier);
            TrailRenderer trail = Instantiate(bulletTrail, bulletExit.position, Quaternion.identity);
            StartCoroutine(BulletTravel(trail, hit));
        }
    }

    void Update() 
    {
        if(Time.timeScale>0)
        {
            if (Input.GetKeyDown(UserSettings.keybinds["attack"]) && canShoot)
            {
                if(ammoInClip==0)
                    StartCoroutine(Reload());
                else
                    FireGun();
            }
            else if(Input.GetKeyDown(UserSettings.keybinds["reload"]) && ammoInClip<ammoCapacity)
                StartCoroutine(Reload());
        }
    }   
}