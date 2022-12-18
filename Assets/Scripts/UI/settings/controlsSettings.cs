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

    movement movement;
    PlayerAction playerAction;

    MouseLook mouseLook;

    public Dictionary<string,KeyCode> inputKeyBinds = new Dictionary<string, KeyCode>() {
        {"walk",KeyCode.W},
        {"left",KeyCode.A},
        {"back",KeyCode.S},
        {"right",KeyCode.D},
        {"jump",KeyCode.Space},
        {"crouch",KeyCode.LeftControl},
        {"sprint",KeyCode.LeftShift},
        {"interact",KeyCode.E},
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

    public void ChangeKey(string action) {
        rmkb.gameObject.SetActive(true); //SetActive(false)-ed in rmkb
        rmkb.targetAction = action; //forward the action to change
    }

    public void updateKeyBinds() { //put this one a button or exit idk. Convert dictionary to script
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

    public void updateButtonText(string action,string newInput) {
        foreach (Transform button in keyBindPanel) {
            if(button.name == action) {
                TextMeshProUGUI childtext = button.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                childtext.text = newInput;
                dynamicButtons(button);
            } 
        }
    }

    void Start() {
        foreach (Transform button in keyBindPanel) {
            dynamicButtons(button);
        }
    }

    void dynamicButtons(Transform button) {
        TextMeshProUGUI childtext = button.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        float len = childtext.text.Length;
        if (len != 1) { //astetic touch
            len = len / 2 + 1;
        }
        RectTransform buttonRTF = (button.GetChild(0) as RectTransform);
        buttonRTF.sizeDelta = new Vector2(50 * len ,50);
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        sensitivitySlider.value = data.sensitivityFlt;
        if(data.inputKeyBinds != null) {
            inputKeyBinds = data.inputKeyBinds;
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
