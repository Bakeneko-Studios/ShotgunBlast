using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private settingsController settingsController; //prefab
    private GameObject UD;
    private GameObject currentlyOpened;
    [SerializeField] private GameObject defaultSelected;

    void Awake() {
        // loadSettings(true);
        currentlyOpened = mainPanel;
        SavingSystem.LoadUser();
        UserSettings.keybinds["pause"] = KeyCode.Escape;
    }

    void Update(){
        if (Input.GetKeyDown(UserSettings.keybinds["pause"]))
        {
            if (!Player.paused)
            {pause();}
            else
            {unpause();}
        }
    }
    public void pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Player.paused=true;
        Time.timeScale = 0;
        mainPanel.SetActive(true);
        HUD.instance.gameObject.SetActive(false); //might add an animaiton here
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(defaultSelected, new BaseEventData(eventSystem)); 
    }
    public void unpause()
    {
        if(currentlyOpened != mainPanel)
        {
            mainPanel.SetActive(true);
            currentlyOpened.SetActive(false);
            currentlyOpened = mainPanel;
            saveVar();
        }
        else
        {
            Time.timeScale = 1f;
            mainPanel.SetActive(false);
            HUD.instance.gameObject.SetActive(true);
            saveVar();
            Player.paused=false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void mainePanelButton(GameObject thatPanel) {
        currentlyOpened=thatPanel;
        currentlyOpened.SetActive(true);
        mainPanel.SetActive(false);
        if(thatPanel==settingsController.gameObject) settingsController.settingsButton(settingsController.subPanels[0]);
    }

    // public void refreshHealthBar() {
    //     if(Player.GetComponent<playerHealth>() != null) {
    //         playerHealth playerHealth = Player.GetComponent<playerHealth>();
    //         playerHealth.healthBar.gameObject.SetActive(!playerHealth.healthBar.gameObject.activeInHierarchy);
    //         float hp = playerHealth.health;
    //         float scale = menuHealthBar.transform.localScale.x / 100;
    //         menuHealthBar.transform.localScale = new Vector3(hp * scale, menuHealthBar.transform.localScale.y, menuHealthBar.transform.localScale.z);
    //     }
    // }

    // void loadSettings(bool destory) {
    //     GameObject tempSettingsController = Instantiate(settingsController, this.transform);
    //     tempSettingsController.GetComponent<settingsController>().onlyLoadSettings = destory;
    // }

    public void saveVar() {
        SavingSystem.SaveUser();
    }

    public void leaveGame() {
        Application.Quit();
    }
}
