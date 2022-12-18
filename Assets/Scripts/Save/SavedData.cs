using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    //Settings Data
    //Graphics
    public int gQualityInt;
    public float rDistanceFlt;
    public float FOVFlt;
    public bool healthBarBool;
    //Audio
    public float masterVolumeFlt;
    public float musicVolumeFlt;
    public float effectsVolumeFlt;
    //Controls
    public float sensitivityFlt;
    public Dictionary<string, KeyCode> inputKeyBinds;

    public SavedData (UserSettings user)
    {
        //Settings Data
        //Graphics
        gQualityInt = user.gQualityInt;
        rDistanceFlt = user.rDistanceFlt;
        FOVFlt = user.FOVFlt;
        healthBarBool = user.healthBarBool;
        //Audio
        masterVolumeFlt = user.masterVolumeFlt;
        musicVolumeFlt = user.musicVolumeFlt;
        effectsVolumeFlt = user.effectsVolumeFlt;
        //Controls
        sensitivityFlt = user.sensitivityFlt;
    }
}
