using UnityEngine;

public class ShotgunPickup : Interactable
{
    private GameObject player;
    private GameObject gunHolder;
    public GameObject gun;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunHolder = GameObject.FindGameObjectWithTag("RHand");
    }
    public override void OnInteract()
    {
        if (gunHolder.transform.childCount>0)
        {
            for (int i=0; i<gunHolder.transform.childCount; i++)
                Destroy(gunHolder.transform.GetChild(i).gameObject);
        }
        //put gun as child of gunHolder
        GameObject myGun = Instantiate(gun,gunHolder.transform);
        // myGun.GetComponent<Shotgun>().enabled = true;
        //Maybe set soecuak transform (diffrent gun sizes)
        myGun.transform.localPosition = Vector3.zero;
        HUD.instance.ChangeGun(true);

        Destroy(this.gameObject);
    }
}
