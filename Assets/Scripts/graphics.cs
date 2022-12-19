using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class graphics : MonoBehaviour
{
    public TMP_Dropdown preset;
    [Range(10,1000)] public Slider renderDistance;
    [Range(40,120)] public Slider FOV;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void reloadGraphics()
    {
        QualitySettings.SetQualityLevel(preset.value,false);
        Camera.main.farClipPlane = renderDistance.value;
        Camera.main.fieldOfView = FOV.value;
    }
}
