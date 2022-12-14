using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private  GameObject settingsController; //prefab
    [SerializeField] private  GameObject userSettings; //prefab
    public GameObject menuHealthBar;
    public GameObject hudHealthBar;
    private GameObject UD;
    private GameObject currentlyOpened;
    private GameObject Player;

    public KeyCode pauseKey = KeyCode.Escape;

    void Awake() {
        if(GameObject.Find("userSettings") == null) {
            UD = Instantiate(userSettings);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        loadSettings(true);
        currentlyOpened = mainPanel;
    }

    void Update(){
        if (Input.GetKeyDown(pauseKey))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                mainPanel.SetActive(true);
                hudHealthBar.SetActive(false);
                // refreshHealthBar();
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
                    hudHealthBar.SetActive(true);
                    saveVar();
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    public void mainePanelButton(GameObject thatPanel) {
        currentlyOpened = Instantiate(thatPanel, this.transform);
        mainPanel.SetActive(false);
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

    void loadSettings(bool destory) {
        GameObject tempSettingsController = Instantiate(settingsController, this.transform);
        tempSettingsController.GetComponent<settingsController>().onlyLoadSettings = destory;
    }

    public void saveVar() {
        SavingSystem.SaveUser(UD.GetComponent<UserSettings>());
    }

    public void leaveGame() {
        Application.Quit();
    }
}
