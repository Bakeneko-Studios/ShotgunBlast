using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemapKeyBind : MonoBehaviour
{

    [SerializeField] private controlsSettings controlsSettings;
    [HideInInspector] public string targetAction;

    KeyCode[] notStringKeyCodes = {
    //on your left
    KeyCode.Tab,KeyCode.CapsLock,KeyCode.LeftShift,KeyCode.LeftControl,KeyCode.LeftAlt,KeyCode.LeftCommand,
    //arrows
    KeyCode.UpArrow,KeyCode.RightArrow,KeyCode.DownArrow,KeyCode.LeftArrow,
    //on your right
    KeyCode.RightShift,KeyCode.RightControl,KeyCode.RightAlt,KeyCode.PageUp,KeyCode.PageDown,KeyCode.RightCommand,
    //mouse
    KeyCode.Mouse1,KeyCode.Mouse2,KeyCode.Mouse3,KeyCode.Mouse4
    };

    void Update() {
        if(Input.anyKeyDown) {
            string newInputstr = Input.inputString;
            if(newInputstr.ToString() != "") { //try string KeyBinds
                foreach(char c in newInputstr) {
                    KeyCode newInput = (KeyCode)c;
                    controlsSettings.inputKeyBinds[targetAction] = newInput;
                    controlsSettings.updateButtonText(targetAction,newInput.ToString(),true);
                }
            } else { //try keybinds in List
                foreach(KeyCode kc in notStringKeyCodes) {
                    if(Input.GetKey(kc)) {
                        KeyCode newInput = kc;
                    controlsSettings.inputKeyBinds[targetAction] = newInput;
                    controlsSettings.updateButtonText(targetAction,newInput.ToString(),true);
                    }
                }
            }
            controlsSettings.GetComponentInParent<PauseScreen>().pauseKey = KeyCode.Escape; //reenable escape
            this.gameObject.SetActive(false);
        }
    }
}
