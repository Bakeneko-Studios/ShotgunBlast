using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsController : MonoBehaviour
{
    [SerializeField] private GameObject[] subPanels;
    public bool onlyLoadSettings;

    void Start() {
        GetComponent<graphicsSettings>().kickStart();
        GetComponent<controlsSettings>().kickStart();
        if (onlyLoadSettings){ 
            Destroy(this.gameObject);
        }
    }

    public void settingsButton(GameObject thisPanel) {
        if(!thisPanel.activeInHierarchy) {
            foreach(GameObject panel in subPanels) {
                if (panel == thisPanel) {
                    panel.SetActive(true);
                } else {
                    panel.SetActive(false);
                }
            }
        } else {
            foreach(GameObject panel in subPanels) {
                panel.SetActive(false);
            }
        }
    }

    void lastOpened() { //Open the menu that was last opened.
        //might code this later.
    }
}
