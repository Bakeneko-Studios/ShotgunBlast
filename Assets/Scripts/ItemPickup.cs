using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private GameObject player;
    private InputManager intputThing;
    private GameObject gunHolder;
    public GameObject gun;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        intputThing = player.GetComponent<InputManager>();
        gunHolder = GameObject.FindGameObjectWithTag("RHand");
    }
    public void onPickUpGun()
    {
        if (gunHolder.transform.childCount==0)
            intputThing.enableGun();
        else{
            for (int i=0; i<gunHolder.transform.childCount; i++)
                Destroy(gunHolder.transform.GetChild(i).gameObject);
        }
        //put gun as child of gunHolder
        GameObject myGun = Instantiate(gun);
        myGun.transform.SetParent(gunHolder.transform);
        //Maybe set soecuak transform (diffrent gun sizes)
        myGun.transform.localPosition = Vector3.zero;
        //Make the gun as the script of the player
        intputThing.shotgun = myGun.GetComponent<Shotgun>();
    }
}
