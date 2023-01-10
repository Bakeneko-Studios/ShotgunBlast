using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class settingsController : MonoBehaviour
{
    [SerializeField] private GameObject[] subPanels;
    public bool onlyLoadSettings;
    public GameObject pointer;
    [SerializeField] private GameObject defaultSelected;

    void Start() {
        if (onlyLoadSettings){ 
            Destroy(this.gameObject);
        }
        EventSystem.current.SetSelectedGameObject(defaultSelected); 
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
            pointer.transform.SetParent(GameObject.Find(thisButtonName).transform) ;
            pointer.transform.localPosition  = new Vector2(-161f,-1f);
        }
        GetComponentInParent<PauseScreen>().saveVar();
    }
}
