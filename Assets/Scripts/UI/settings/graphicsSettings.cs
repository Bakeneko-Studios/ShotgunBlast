using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class graphicsSettings : MonoBehaviour
{
    // [SerializeField] private UniversalRenderPipelineAsset _presetLow;
    // [SerializeField] private UniversalRenderPipelineAsset _presetMedium;
    // [SerializeField] private UniversalRenderPipelineAsset _presetHigh;
    
    [SerializeField] private TMP_Dropdown preset;
    [SerializeField] private GameObject graphicsPanel;

    private Camera cam;

    void Awake() {
        if(SavingSystem.LoadUser() != null) {
            loadVar();
        }
        updateGraphics();
    }

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if(graphicsPanel.activeInHierarchy) {
            updateGraphics();
        }
        saveVar();
    }

    void updateGraphics() {
        QualitySettings.SetQualityLevel(preset.value,false);
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        preset.value = data.gQualityInt;
    }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.gQualityInt = preset.value;
    }
}
