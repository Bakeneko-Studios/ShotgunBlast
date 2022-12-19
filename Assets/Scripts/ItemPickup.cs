using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private GameObject player;

    private GameObject gunHolder;
    public GameObject gun;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunHolder = GameObject.FindGameObjectWithTag("RHand");
    }
    public void onPickUpGun()
    {
        if (gunHolder.transform.childCount>0)
        {
            for (int i=0; i<gunHolder.transform.childCount; i++)
                Destroy(gunHolder.transform.GetChild(i).gameObject);
        }
        //put gun as child of gunHolder
        GameObject myGun = Instantiate(gun);
        myGun.GetComponent<Shotgun>().enabled = true;
        myGun.transform.SetParent(gunHolder.transform);
        //Maybe set soecuak transform (diffrent gun sizes)
        myGun.transform.localPosition = Vector3.zero;
    }
}
