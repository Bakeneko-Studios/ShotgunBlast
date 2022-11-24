using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    //data not need to save
    public int beforeSettings;
    public bool reloadActivate = true;

    //Settings Data
    //Graphics
    public int gQualityInt;
    public float rDistanceFlt;
    public bool healthBarBool;
    public float FOVFlt;
    //Audio
    public float masterVolumeFlt;
    public float musicVolumeFlt;
    public float effectsVolumeFlt;
    //Controls
    public float sensitivityFlt;

    void Awake()
    {
        GameObject[] dataCarriers = GameObject.FindGameObjectsWithTag("userSettings");
        if (dataCarriers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }
}
