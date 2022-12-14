using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controlsSettings : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    [SerializeField] private GameObject controlsPanel;

    void Awake()
    {
        loadVar();
        updateSensitivity();
    }

    public void updateSensitivity() { //put on slider
        MouseLook.sensitivity = sensitivitySlider.value*10;
        sensitivityText.text = sensitivitySlider.value.ToString();
        saveVar();
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        //sensitivitySlider.value = data.sensitivityFlt;
        Debug.Log("loaded" + data.sensitivityFlt);
        }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.sensitivityFlt = sensitivitySlider.value;
    }
}
