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
    [SerializeField] private Slider renderDistance;
    [SerializeField] private Slider FOV;

    [SerializeField] private GameObject graphicsPanel;

    private Camera cam;

    public void kickStart() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        loadVar();
        updateGraphics();
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
        cam.farClipPlane = renderDistance.value;
        cam.fieldOfView = FOV.value;
    }

    void loadVar() {
        SavedData data = SavingSystem.LoadUser();
        renderDistance.value = data.rDistanceFlt;
        FOV.value = data.FOVFlt;
        preset.value = data.gQualityInt;
    }

    void saveVar() {
        UserSettings UD = GameObject.FindGameObjectWithTag("userSettings").GetComponent<UserSettings>();
        UD.rDistanceFlt = renderDistance.value;
        UD.FOVFlt = FOV.value;
        UD.gQualityInt = preset.value;
    }
}
