using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public static playerHealth instance;
    public static bool infiniteHealth;
    public float health = 100;
    public float maxHealth = 100;
    public float maxHealthCur;
    public Image hudHealthBar;
    // public Image menuHealthBar;
    public TextMeshProUGUI hpText;
    private float scale;
    //public GameObject deathPanel;

    void Start()
    {
        instance=this;
        devReload();
    }
    public void devReload()
    {
        if(infiniteHealth) maxHealthCur=health=float.MaxValue;
        else maxHealthCur = maxHealth;
        ChangeHealth(0);
    }
    public void ChangeHealth(float amount)
    {
        health += amount;
        if(health>maxHealthCur) health=maxHealthCur;

        hudHealthBar.fillAmount = health/maxHealthCur;
        // menuHealthBar.fillAmount = health/maxHealth;
        hpText.text = health+" / "+maxHealthCur;
        if (health <= 0)
        {
            Debug.Log("ded");
            //deathPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}
