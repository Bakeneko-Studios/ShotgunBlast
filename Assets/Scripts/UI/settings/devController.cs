using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class devController : MonoBehaviour
{
    public Toggle dbh,ifh,efc,ifa;
    private GameObject hud;
    public void devReload()
    {
        hud.SetActive(dbh.isOn);
        playerHealth.infiniteHealth=ifh.isOn;
        playerHealth.instance.devReload();
        freecam.instance.enabled = efc.isOn;
        //ArmManager.infiniteArms=ifa.isOn;
        //ArmManager.instance.devReload();
    }

    void Awake()
    {
        hud = F3Debug.instance.debugPanel;
        if(!Player.dev)
        {
            UserSettings.debugHUD=UserSettings.infiniteHealth=UserSettings.enableFreecam=UserSettings.infiniteArms=false;
            devReload();
            this.gameObject.SetActive(false);
        }
        else loadVar();
    }
    
    public void saveVar()
    {
        UserSettings.debugHUD=dbh.isOn;
        UserSettings.infiniteHealth=ifh.isOn;
        UserSettings.enableFreecam=efc.isOn;
        UserSettings.infiniteArms=ifa.isOn;
        devReload();
        SavingSystem.SaveUser();
    }
    void loadVar()
    {
        dbh.isOn = UserSettings.debugHUD;
        ifh.isOn = UserSettings.infiniteHealth;
        efc.isOn = UserSettings.enableFreecam;
        ifa.isOn = UserSettings.infiniteArms;
        devReload();
    }
    public void resetVar()
    {
        UserSettings.resetDev();
    }
}
