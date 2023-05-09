using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_StandardV2 : PistolV2
{
    protected override void Start()
    {
        base.Start();
        canShoot = true;
    }

    public override void FireGun()
    {
        ammoInClip--;
        Physics.Raycast(bulletExit.position,cam.transform.forward, out RaycastHit hit, float.PositiveInfinity);
        if(hit.collider.TryGetComponent<enemyFramework>(out enemyFramework ef))
            ef.ChangeHealth(-damage*Player.damageMultiplier);
        else if(hit.collider.TryGetComponent<Dummy>(out Dummy d))
            d.minusHP(-damage*Player.damageMultiplier);
        else if(hit.collider.TryGetComponent<Shield>(out Shield s))
            s.StartCoroutine(s.takeDamage(damage*Player.damageMultiplier));
        TrailRenderer trail = Instantiate(bulletTrail, bulletExit.position, Quaternion.identity);
        StartCoroutine(BulletTravel(trail, hit));
    }
    void Update() 
    {
        if(Time.timeScale>0)
        {
            if (Input.GetKeyDown(UserSettings.keybinds["attack"]) && canShoot)
            {
                FireGun();
                if (ammoInClip == 0)
                    StartCoroutine(Reload());
            }
            else if(Input.GetKeyDown(UserSettings.keybinds["reload"]) && ammoInClip<ammoCapacity)
                StartCoroutine(Reload());
        }
    }
}
