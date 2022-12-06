using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float health = 100;
    public GameObject healthBar;
    private float scale;
    //public GameObject deathPanel;

    public void ChangeHealth(float amount)
    {
        // Change the health by the amount specified in the amount variable

        health += amount;

        healthBar.transform.localScale = new Vector3(health * scale, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health <= 0)
        {
            //deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

    }
    void Start()
    {
        scale = healthBar.transform.localScale.x / 100;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
