using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public bool infiniteHealth;
    public float health = 100;
    public float maxHealth;
    public Image hudHealthBar;
    public Image menuHealthBar;
    private float scale;
    //public GameObject deathPanel;

    void Awake()
    {if(!Player.dev) infiniteHealth=false;}

    public void ChangeHealth(float amount)
    {
        health += amount;
        if(health>maxHealth) health=maxHealth;

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
        if(infiniteHealth) maxHealth=health=float.MaxValue;
        else health=maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
