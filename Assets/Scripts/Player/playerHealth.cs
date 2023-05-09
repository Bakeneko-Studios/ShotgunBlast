using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class playerHealth : MonoBehaviour
{
    public static playerHealth instance;
    public static bool infiniteHealth;
    public int bigHealth = 5;
    public float health = 100;
    public float maxHealthCur;
    private float scale;
    public float healCooldown;
    public float timeBetweenHeals;
    private float cd;
    public float naturalRegen;


    public UnityEvent OnBigBreak;


    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        devReload();
        InvokeRepeating("heal",2f,timeBetweenHeals);
        cd=healCooldown;
        OnBigBreak.AddListener(KnockBack);
        
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
                if (bigHealth > 1)
                {
                    HUD.instance.ChangeCell(bigHealth, false);
                    bigHealth -= 1;

                    //Player.invincible = true;//set invincible time
                    OnBigBreak.Invoke();
                    
                    ChangeHealth(maxHealthCur-health);
                }
                else//u die
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

    //OnBigBreak.AddListener();
    public void KnockBack()
    {
        Debug.Log("shockwave called");
        float shockRad = 7f;
        float shockMag = 999f;
        Collider[] EnemiesInRange = Physics.OverlapSphere(this.transform.position, shockRad, 0);

        foreach (Collider Enemy in EnemiesInRange)
        {
            float idfk = shockRad - Vector3.Distance(Enemy.transform.position, this.transform.position);
            Enemy.GetComponent<Rigidbody>().AddForce((Enemy.transform.position - this.transform.position)*shockMag, ForceMode.Impulse);
        }
    }
}
