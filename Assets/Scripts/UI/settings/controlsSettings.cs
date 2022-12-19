using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controlsSettings : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private Transform keyBindPanel;
    [SerializeField] private RemapKeyBind rmkb;
    [SerializeField] private GameObject errorPanel;

    movement movement;
    PlayerAction playerAction;

    MouseLook mouseLook;

    public Dictionary<string,KeyCode> inputKeyBinds = new Dictionary<string, KeyCode>() {
        {"walk",KeyCode.W},
        {"left",KeyCode.A},
        {"back",KeyCode.S},
        {"right",KeyCode.D},
        {"jump",KeyCode.Space},
        {"crouch",KeyCode.C},
        {"sprint",KeyCode.LeftShift},
        {"interact",KeyCode.E},
        {"pause",KeyCode.Escape}
    };

    void Awake()
    {
        context();
        loadVar();
        updateSensitivity();
        updateKeyBinds();
    }

    void context() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        mouseLook = player.GetComponent<MouseLook>();
        movement = player.GetComponent<movement>();
        playerAction = player.GetComponent<PlayerAction>();
    }

    public void updateSensitivity() { //put on slider
        mouseLook.sensitivity = sensitivitySlider.value*10;
        sensitivityText.text = sensitivitySlider.value.ToString();
        saveVar();
    }

    Dictionary<string,KeyCode> inputKeyBindsClone; //create a clone incase error

    public void ChangeKey(string action) {
        inputKeyBindsClone = inputKeyBinds; //cloning dictionaries
        rmkb.gameObject.SetActive(true); //SetActive(false)-ed in rmkb
        rmkb.targetAction = action; //forward the action to change
        GetComponentInParent<PauseScreen>().pauseKey = KeyCode.None;
    }

    public void updateKeyBinds() { //put this one a button or exit idk. Convert dictionary to script
        //check for duplicates
        bool duplicates = false;
        var dupe = inputKeyBinds.GroupBy(x => x.Value);
        foreach (var group in dupe) {
            if (group.Count() > 1)
            {
                duplicates = true;
                string message = group.Key.ToString();
                inputKeyBinds = inputKeyBindsClone; //revert changes
                errorPanel.SetActive(true);
                Debug.Log(message);
                break;
            }
        }
        //if no duplicates send values to scripts
        if(!duplicates) {
            //movement keybinds
            movement.walkKey = inputKeyBinds["walk"];
            movement.leftKey = inputKeyBinds["left"];
            movement.backKey = inputKeyBinds["back"];
            movement.rightKey = inputKeyBinds["right"];
            //parkour keybinds
            movement.jumpKey = inputKeyBinds["jump"];
            movement.sprintKey = inputKeyBinds["sprint"];
            movement.crouchKey = inputKeyBinds["crouch"];
            //PlayerAction keybind
            playerAction.interactKey = inputKeyBinds["interact"];;
            saveVar();
        }
    }

    float buttonScale = 50f;
    float buttonPosx = 217.4f;  
    public void updateButtonText(string action,string newInput) {
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
            } 
        }
    }

    void Start() {
        foreach (var item in inputKeyBinds) {
            updateButtonText(item.Key,item.Value.ToString());
        }
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        sensitivitySlider.value = data.sensitivityFlt;
        if(data.inputKeyBinds != null) {
            inputKeyBinds = data.inputKeyBinds;
        }
        else {
            Debug.Log("fuck it's not working");
        }
    }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        //sensitivity
        UD.sensitivityFlt = sensitivitySlider.value;
        //movement keybinds
        UD.inputKeyBinds = inputKeyBinds;
    }
}
