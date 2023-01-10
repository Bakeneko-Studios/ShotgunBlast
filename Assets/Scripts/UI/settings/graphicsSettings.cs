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
    [SerializeField] private Slider FOV;

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
        // if(graphicsPanel.activeInHierarchy) {
        //     updateGraphics();
        // }
        // saveVar();
    }

    public void updateGraphics() {
        QualitySettings.SetQualityLevel(preset.value,false);
        Debug.Log(QualitySettings.GetQualityLevel());
        Camera.main.fieldOfView = FOV.value;
        saveVar();
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        preset.value = data.gQualityInt;
    }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.gQualityInt = preset.value;
    }

    public void resettVar() {
        preset.value = 3;
        saveVar();
    }
}
