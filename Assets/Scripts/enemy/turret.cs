using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    public GameObject projectile;
    public Transform[] firePoints;
    public GameObject gun;
    public float projectileSpeed = 1000;
    public GameObject player;

    public void attack(Transform player)
    {
        // add animations and sounds later
        Debug.Log("attacking");
        for (int i = 0; i < firePoints.Length; i++)
        {
            //firePoints[i].LookAt(new Vector3(player.position.x, player.position.y, player.position.z));
            GameObject shot = Instantiate(projectile, firePoints[i].position, firePoints[i].rotation);
            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * projectileSpeed);
        }
    }

    private void aimGuns()
    {
        //add vertical aim
    }

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<enemyFramework>().playerInSightRange)
        {
            aimGuns();
        }
    }
}
