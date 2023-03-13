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
    public float healCooldown;
    public float timeBetweenHeals;
    private float cd;
    public float naturalRegen;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        devReload();
        InvokeRepeating("heal",2f,timeBetweenHeals);
        cd=healCooldown;
    }
    void Update()
    {
        if(cd>0) cd-=Time.deltaTime;
    }
    public void devReload()
    {
        if(infiniteHealth) maxHealthCur=health=float.MaxValue;
        else maxHealthCur = maxHealth;
        ChangeHealth(0);
    }
    public void ChangeHealth(float amount)
    {
        if(amount>0 && health<maxHealthCur) ScreenFlash.instance.RegenFlash();
        else if(amount<0)
        {
            ScreenFlash.instance.Flash("dmgColor");
            cd=healCooldown;
        }
        
        health += amount;
        if(health>maxHealthCur) health=maxHealthCur;

        HUD hudscript = HUD.instance;
        if(hudscript.gameObject.activeInHierarchy) hudscript.ChangeHealth(health/maxHealthCur);
        // hudscript.menuHealthBar.fillAmount = health/maxHealth;
        if (health <= 0)
        {
            ScreenFlash.instance.Flash("deathColor");
            CancelInvoke();
            Debug.Log("ded");
            hudscript.hpText.text = "0 / "+maxHealthCur; //prevent neative health numbers becayse that is stupid
            DeathUI.instance.deathPanel.SetActive(true);
            DeathUI.instance.deathEvent();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            hudscript.hpText.text = health+" / "+maxHealthCur;
        }
    }
    void heal()
    {
        if(cd<=0) ChangeHealth(naturalRegen);
    }
}
