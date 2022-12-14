using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health = 100;
    float maxHealth;
    public Image hudHealthBar;
    public Image menuHealthBar;
    private float scale;
    //public GameObject deathPanel;

    public void ChangeHealth(float amount)
    {
        if(health>maxHealth) maxHealth=health;
        // Change the health by the amount specified in the amount variable

        health += amount;

        hudHealthBar.fillAmount=health/maxHealth;
        menuHealthBar.fillAmount=health/maxHealth;
        if (health <= 0)
        {
            Debug.Log("ded");
            //deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

    }
    void Start()
    {
        // scale = healthBar.transform.localScale.x / 100;
        maxHealth=health;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
