using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private  GameObject settingsController; //prefab
    [SerializeField] private  GameObject userSettings; //prefab
    private GameObject UD;
    private GameObject currentlyOpened;


    void Awake() {
        if(GameObject.Find("userSettings") == null) {
            UD = Instantiate(userSettings);
        }
        loadSettings(true);
        currentlyOpened = mainPanel;
    }

    public void OnEscPressed() {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            mainPanel.SetActive(true);
        }
        else
        {
            if(currentlyOpened != mainPanel) {
                mainPanel.SetActive(true);
                Destroy(currentlyOpened);
                currentlyOpened = mainPanel;
            } else {
                Time.timeScale = 1f;
                mainPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                saveVar();
            }
        }
    }

    public void mainePanelButton(GameObject thatPanel) {
        currentlyOpened = Instantiate(thatPanel, this.transform);
        mainPanel.SetActive(false);
    }

    void loadSettings(bool destory) {
        GameObject tempSettingsController = Instantiate(settingsController, this.transform);
        tempSettingsController.GetComponent<settingsController>().onlyLoadSettings = destory;
    }

    public void saveVar() {
        SavingSystem.SaveUser(UD.GetComponent<UserSettings>());
    }
}
