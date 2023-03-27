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
        else maxHealthCur = Player.maxHealth;
        ChangeHealth(0);
    }
    public void ChangeHealth(float amount)
    {
        if(!Player.invincible)
        {
            if(amount>0 && health<maxHealthCur) ScreenFlash.instance.RegenFlash();
            else if(amount<0)
            {
                ScreenFlash.instance.Flash("dmgColor");
                cd=healCooldown;
            }
            
            health += amount * (amount<0?(1-Player.damageResistance):1);
            if(health>maxHealthCur) health=maxHealthCur;

            if(HUD.instance.gameObject.activeInHierarchy) HUD.instance.ChangeHealth(health/maxHealthCur);
            // hudscript.menuHealthBar.fillAmount = health/maxHealth;
            if (health <= 0)
            {
                ScreenFlash.instance.Flash("deathColor");
                CancelInvoke();
                Debug.Log("ded");
                HUD.instance.hpText.text = "0 / "+maxHealthCur; //prevent neative health numbers becayse that is stupid
                DeathUI.instance.deathPanel.SetActive(true);
                DeathUI.instance.deathEvent();
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Player.ChangeBallz('d',2);
            }
            else
            {
                HUD.instance.hpText.text = health+" / "+maxHealthCur;
            }
        }
    }
    void heal()
    {
        if(cd<=0) ChangeHealth(naturalRegen);
    }
}
