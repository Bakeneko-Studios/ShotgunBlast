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
    [SerializeField] private RemapKeyBind rmkb;

    movement movement;
    PlayerAction playerAction;

    MouseLook mouseLook;

    public Dictionary<string,KeyCode> inputKeyBinds = new Dictionary<string, KeyCode>() {
        // {"forward",KeyCode.W},
        // {"back",KeyCode.S},
        // {"left",KeyCode.A},
        // {"right",KeyCode.D},
        {"jump",KeyCode.Space},
        {"crouch",KeyCode.LeftControl},
        {"dash",KeyCode.LeftShift},
        {"pause",KeyCode.Escape},
    };

    void Awake()
    {
        context();
        loadVar();
        updateSensitivity();
        //updateKeyBinds();
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
        movement.jumpKey = inputKeyBinds["jump"];
        movement.sprintKey = inputKeyBinds["sprint"];
        movement.crouchKey = inputKeyBinds["crouch"];
        //PlayerAction keybind
        playerAction.interactKey = inputKeyBinds["interact"];;
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        sensitivitySlider.value = data.sensitivityFlt;
        if(data.inputKeyBinds != null) {
            data.inputKeyBinds.ToList().ForEach(x => inputKeyBinds.Add(x.Key, x.Value)); //merge dictionaries I hope I did this correctly
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
