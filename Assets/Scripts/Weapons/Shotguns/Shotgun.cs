using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shotgun : Gun
{
    public int pelletCount;
    public List<Quaternion> pelletAngles;
    public float cameraShakeDuration = 0.3f;
    public float cameraShakeMagnitude = 0.6f;
    protected IEnumerator ReloadSingle()
    {
        ArmManager.isBusy=true;
        canShoot=false;
        while(ammoInClip<ammoCapacity)
        {
            yield return new WaitForSeconds(reloadTime/Player.firerateMultiplier);
            ammoInClip++;
            canShoot=true;
        }
        canShoot=true;
        ArmManager.isBusy=false;
    }
}
