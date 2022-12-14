using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsController : MonoBehaviour
{
    [SerializeField] private GameObject[] subPanels;
    public GameObject pointer;

    void Start() {

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
            
            string thisButtonName = thisPanel.name.Replace("Panel","Button");
            pointer.transform.parent = GameObject.Find(thisButtonName).transform;
            pointer.transform.localPosition  = new Vector2(-161f,-1f);
        }
    }
}
