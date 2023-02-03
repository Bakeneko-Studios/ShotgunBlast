using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private  GameObject settingsController; //prefab
    public GameObject menuHealthBar;
    public GameObject hudHealthBar;
    private GameObject UD;
    private GameObject currentlyOpened;
    private GameObject Player;

    public KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private GameObject defaultSelected;

    void Awake() {
        loadSettings(true);
        Player = GameObject.FindGameObjectWithTag("Player");
        currentlyOpened = mainPanel;
    }

    void Update(){
        if (Input.GetKeyDown(pauseKey))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Player.GetComponent<movement>().enabled = false;
                Time.timeScale = 0;
                mainPanel.SetActive(true);
                hudHealthBar.SetActive(false);
                // refreshHealthBar();
                EventSystem.current.SetSelectedGameObject(defaultSelected); 
            }
            else
            {
                if(currentlyOpened != mainPanel) {
                    mainPanel.SetActive(true);
                    Destroy(currentlyOpened);
                    currentlyOpened = mainPanel;
                    saveVar();
                } else {
                    Time.timeScale = 1f;
                    mainPanel.SetActive(false);
                    hudHealthBar.SetActive(true);
                    saveVar();
                    Player.GetComponent<movement>().enabled = true;
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
        SavingSystem.SaveUser();
    }

    public void leaveGame() {
        Application.Quit();
    }
}
