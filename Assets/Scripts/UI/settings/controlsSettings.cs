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

    public void kickStart()
    {
        loadVar();
        updateSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsPanel.activeInHierarchy) {
            updateSensitivity();
        }
        saveVar();
    }

    void updateSensitivity() {
        MouseLook.sensitivity = sensitivitySlider.value / 100f;
        sensitivityText.text = sensitivitySlider.value.ToString();
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        sensitivitySlider.value = data.sensitivityFlt;
    }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.sensitivityFlt = sensitivitySlider.value;
    }
}
