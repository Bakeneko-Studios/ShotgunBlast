using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    public Shotgun shotgun;
    public GameObject newPellet;

    public GameObject bulletNote;
    public GameObject fireNote;

    public void ChangePellet()
    {
        shotgun.pellet = newPellet;
        StartCoroutine(NotifyWait(bulletNote));
    }
    public void QuickFire()
    {
        shotgun.isAnimate = false;
        StartCoroutine(NotifyWait(fireNote));
    }
    IEnumerator NotifyWait(GameObject note)
    {
        note.SetActive(true);
        yield return new WaitForSeconds(5);
        note.SetActive(false);
    }
}
