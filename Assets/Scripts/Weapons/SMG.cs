// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SMG : MonoBehaviour
// {
//     public int maxAmmo;
//     public int cAmmo;
//     public float bulletSpeed;
//     public float reloadTime;
//     public GameObject bullet;

//     public bool canShoot;
//     protected Transform cam;
//     void Start()
//     {
//         cam = Camera.main.transform;
//         //anim = GetComponentInChildren<Animation>();
//         //isAnimate = anim!=null;
//     }
//     public void FireGun()
//     {
        

//         if (cAmmo == 0)
//         {
//             canShoot=false;
//             // if(isAnimate && !ArmManager.isBusy)
//             //     GetComponent<Animator>().SetTrigger("spin");
//             // else if(isAnimate)
//             //     animOneHand.Play();
//             StartCoroutine(Reload());
//         }
//     }

//     IEnumerator Reload()
//     {
//         ArmManager.isBusy=true;
//         yield return new WaitForSeconds(reloadTime/Player.firerateMultiplier);
//         cAmmo = maxAmmo;
//         canShoot = true;
//         ArmManager.isBusy=false;
//     }

//     void Update() 
//     {
//         if (Input.GetKey(UserSettings.keybinds["attack"]) && canShoot && Time.timeScale>0)
//             FireGun();
//     }
// }
