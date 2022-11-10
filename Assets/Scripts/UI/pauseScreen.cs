using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject[] subPanels;
    public GameObject[] hideOnEsc;

    [SerializeField] private  GameObject userSettings; //prefab
    private GameObject UD;


    void Awake() {
        if(GameObject.Find("userSettings") == null) {
            UD = Instantiate(userSettings);
        }
    }

    public void OnEscPressed() {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            mainPanel.SetActive(true);
            escHide();
        }
        else
        {
            mainPanel.GetComponent<mainPanelAnimations>().mainRetractQuick();
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            foreach(GameObject panel in subPanels) {
                panel.SetActive(false);
            }
            escShow();
            saveVar();
        }
    }

    public void menuButton(GameObject thisPanel) {
        if(!thisPanel.activeInHierarchy) {
            mainPanel.GetComponent<mainPanelAnimations>().mainExpand();
            foreach(GameObject panel in subPanels) {
                if (panel == thisPanel) {
                    panel.SetActive(true);
                } else {
                    panel.SetActive(false);
                }
            }
        } else {
            mainPanel.GetComponent<mainPanelAnimations>().mainRetract();
            foreach(GameObject panel in subPanels) {
                panel.SetActive(false);
            }
        }
        saveVar();
    }

    public void escHide() {
        foreach(GameObject hide in hideOnEsc) {
            hide.SetActive(false);
        }
    }
    public void escShow() {
        foreach(GameObject hide in hideOnEsc) {
            hide.SetActive(true);
        }
    }

    public void saveVar() {
        GetComponent<graphicsSettings>().saveVar();
        GetComponent<audioSettings>().saveVar();
        GetComponent<controlsSettings>().saveVar();
        GetComponent<detailsSettings>().saveVar();
        SavingSystem.SaveUser(UD.GetComponent<UserSettings>());
    }
}
