using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class controlsSettings : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private Transform keyBindPanel;
    [SerializeField] private RemapKeyBind rmkb;
    [SerializeField] private GameObject errorPanel;
    PlayerAction playerAction;

    void Awake()
    {
        context();
        loadVar();
    }

    void context() {
        GameObject player = movement.instance.gameObject;
        playerAction = player.GetComponent<PlayerAction>();
    }

    public void updateSensitivity() { //put on slider
        MouseLook.instance.sensitivity = sensitivitySlider.value*10;
        sensitivityText.text = sensitivitySlider.value.ToString();
        SavingSystem.SaveUser();
    }

    Dictionary<string,KeyCode> keybindsClone = UserSettings.keybinds; //create a clone incase error

    public void ChangeKey(string action) {
        rmkb.gameObject.SetActive(true); //SetActive(false)-ed in rmkb
        rmkb.targetAction = action; //forward the action to change
        UserSettings.keybinds["pause"] = KeyCode.None;
        SavingSystem.SaveUser();
    }

    // public void updateKeyBinds() { //put this one a button or exit idk. Convert dictionary to script
    //     //check for duplicates
    //     bool duplicates = false;
    //     var dupe = UserSettings.keybinds.GroupBy(x => x.Value);
    //     foreach (var group in dupe) {
    //         if (group.Count() > 1)
    //         {
    //             duplicates = true;
    //             string message = group.Key.ToString();
    //             UserSettings.keybinds = keybindsClone; //revert changes
    //             errorPanel.SetActive(true);
    //             Debug.Log(message);
    //             break;
    //         }
    //     }
    //     //if no duplicates send values to scripts
    //     if(!duplicates) {
    //         saveVar();
    //     }
    // }

    float buttonScale = 50f;
    float buttonPosx = 217.4f;  
    public void updateButtonText(string action,string newInput,bool select) {
        foreach (Transform button in keyBindPanel) {
            if(button.name == action) {
                TextMeshProUGUI childtext = button.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                childtext.text = newInput;
                //dynamicButtons
                float len = newInput.Length;
                if (len != 1) { //astetic touch
                    len = (len + 1f) / 2f;
                }
                RectTransform buttonRTF = (button.GetChild(0) as RectTransform);
                buttonRTF.sizeDelta = new Vector2(buttonScale * len ,buttonScale);
                buttonRTF.localPosition = new Vector2(buttonPosx + buttonScale*len/2, buttonRTF.localPosition.y);
                if (select) EventSystem.current.SetSelectedGameObject(button.GetChild(0).gameObject);
            } 
        }
    }

    public void resetKeyBinds()
    {
        UserSettings.resetKeybinds();
        loadVar();
    }
    void loadVar() {
        sensitivitySlider.value = UserSettings.sensitivityFlt;
        MouseLook.instance.sensitivity = sensitivitySlider.value*10;
        sensitivityText.text = sensitivitySlider.value.ToString();
        foreach (var item in UserSettings.keybinds)
        {
            updateButtonText(item.Key,item.Value.ToString(),false);
        }
    }

    public void resetVar() {
        UserSettings.resetControls();
    }
}
