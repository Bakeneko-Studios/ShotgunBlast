using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private  GameObject settingsController; //prefab
    [SerializeField] private  GameObject userSettings; //prefab
    public GameObject menuHealthBar;
    private GameObject UD;
    private GameObject currentlyOpened;
    private GameObject Player;


    void Awake() {
        if(GameObject.Find("userSettings") == null) {
            UD = Instantiate(userSettings);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<InputManager>().pauseScreen = GetComponent<pauseScreen>();
        currentlyOpened = mainPanel;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            OnEscPressed();
        }
    }

    void OnEscPressed() {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            mainPanel.SetActive(true);
            refreshHealthBar();
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

    public void refreshHealthBar() {
        if(Player.GetComponent<playerHealth>() != null) {
            playerHealth playerHealth = Player.GetComponent<playerHealth>();
            playerHealth.healthBar.SetActive(false);
            float hp = playerHealth.health;
            float scale = menuHealthBar.transform.localScale.x / 100;
            menuHealthBar.transform.localScale = new Vector3(hp * scale, menuHealthBar.transform.localScale.y, menuHealthBar.transform.localScale.z);
        }
    }

    void loadSettings() {
        GameObject tempSettingsController = Instantiate(settingsController, this.transform);
    }

    public void saveVar() {
        SavingSystem.SaveUser(UD.GetComponent<UserSettings>());
    }

    public void leaveGame() {
        Application.Quit();
    }
}
