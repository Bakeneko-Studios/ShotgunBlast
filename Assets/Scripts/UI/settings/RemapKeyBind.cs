using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemapKeyBind : MonoBehaviour
{

    [SerializeField] private controlsSettings controlsSettings;
    [HideInInspector] public string targetAction;

    void Start() {
        Debug.Log("Yes");
    }

    void Update() {
        if(Input.anyKeyDown) {
            //find a new KeyBind
            string newInputstr = Input.inputString;
            foreach(char c in newInputstr) {
                KeyCode newInput = (KeyCode)c;
                Debug.Log(newInput.ToString());
                controlsSettings.inputKeyBinds.Add(targetAction,newInput);
            }
            this.gameObject.SetActive(false);
        }
    }
}
