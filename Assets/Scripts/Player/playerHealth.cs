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
    private float scale;
    public GameObject deathPanel;

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

        HUD hudscript = HUD.instance;
        hudscript.hudHealthBar.fillAmount = health/maxHealthCur;
        // hudscript.menuHealthBar.fillAmount = health/maxHealth;
        if (health <= 0)
        {
            Debug.Log("ded");
            hudscript.hpText.text = "0 / "+maxHealthCur; //prevent neative health numbers becayse that is stupid
            DeathUI.instance.deathPanel.SetActive(true);
            DeathUI.instance.deathEvent();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        } else {
            hudscript.hpText.text = health+" / "+maxHealthCur;
        }
    }
}
