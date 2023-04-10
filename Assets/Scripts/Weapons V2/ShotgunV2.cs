using System.Collections;
using UnityEngine;

public abstract class ShotgunV2 : GunV2
{
    public int raycastCount;
    public bool fixedPattern;
    [SerializeField] protected Vector3[] raycastAngles;
    public float cameraShakeDuration = 0.3f;
    public float cameraShakeMagnitude = 0.6f;
    protected IEnumerator ReloadSingle()
    {
        if(isAnimate && !ArmManager.isBusy)
            anim.SetTrigger("spin");
        else if(isAnimate)
            anim.SetTrigger("spin");
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
    protected void GenerateRandomSpread()
    {
        raycastAngles = new Vector3[raycastCount];
        for (int i = 0; i < raycastAngles.Length; i++)
            raycastAngles[i] =  new Vector3(
                Random.Range(-spreadAngle.x,spreadAngle.x),
                Random.Range(-spreadAngle.y,spreadAngle.y),
                Random.Range(-spreadAngle.z,spreadAngle.z));
    }
}
