using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    //Settings Data
    //Graphics
    public int gQualityInt;
    public float FOVFlt;
    public bool healthBarBool;
    //Audio
    public float masterVolumeFlt;
    public float musicVolumeFlt;
    public float effectsVolumeFlt;
    //Controls
    public float sensitivityFlt;
    public Dictionary<string, KeyCode> inputKeyBinds;
    //Dev
    public bool debugHUD,infiniteHealth,enableFreecam,infiniteArms;

    //Player Data
    public int ballz;

    public SavedData()
    {
        //Settings Data
        //Graphics
        gQualityInt = UserSettings.gQualityInt;
        FOVFlt = UserSettings.FOVFlt;
        healthBarBool = UserSettings.healthBarBool;
        //Audio
        masterVolumeFlt = UserSettings.masterVolumeFlt;
        musicVolumeFlt = UserSettings.musicVolumeFlt;
        effectsVolumeFlt = UserSettings.effectsVolumeFlt;
        //Controls
        sensitivityFlt = UserSettings.sensitivityFlt;
        inputKeyBinds = UserSettings.keybinds;
        //Dev
        debugHUD = UserSettings.debugHUD;
        infiniteHealth = UserSettings.infiniteHealth;
        enableFreecam = UserSettings.enableFreecam;
        infiniteArms = UserSettings.infiniteArms;

        //Player Data
        ballz = Player.ballz;
    }
    public override string ToString()
    {
        return $"Quality Level: {gQualityInt}\nFOV: {FOVFlt}\nShow Health Bars: {healthBarBool}\nMaster Volume: {masterVolumeFlt}\nMusic Volume: {musicVolumeFlt}\nEffects Volume: {effectsVolumeFlt}\nSensitivity: {sensitivityFlt}\nKeybinds: {inputKeyBinds}";
    }
}
