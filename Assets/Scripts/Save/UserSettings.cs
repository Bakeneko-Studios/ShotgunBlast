using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserSettings
{
    //data not need to save
    public static int beforeSettings;
    public static bool reloadActivate = true;

    //Settings Data
    //Graphics
    public static int gQualityInt = 3;
    public static bool healthBarBool;
    public static float FOVFlt = 80f;
    //Audio
    public static float masterVolumeFlt = 1f;
    public static float musicVolumeFlt = 1f;
    public static float effectsVolumeFlt = 1f;
    //Controls
    public static float sensitivityFlt = 70f;

    //Game data
    public static List<Arm> unlockedArms = new List<Arm>();

    public static Dictionary<string, KeyCode> keybinds = new Dictionary<string, KeyCode>()
    {
        {"forward",KeyCode.W},
        {"left",KeyCode.A},
        {"back",KeyCode.S},
        {"right",KeyCode.D},
        {"jump",KeyCode.Space},
        {"crouch",KeyCode.C},
        {"interact",KeyCode.E},
        {"pause",KeyCode.Escape},
        {"attack",KeyCode.Mouse0},
        {"charge",KeyCode.Mouse1},
        {"reload",KeyCode.R},
        {"punch",KeyCode.V},
        {"ability",KeyCode.Q},
        {"freecam",KeyCode.Z},
        {"cycle arm",KeyCode.X},
    };

    public static void resetKeybinds()
    {
        keybinds = new Dictionary<string, KeyCode>()
        {
            {"forward",KeyCode.W},
            {"left",KeyCode.A},
            {"back",KeyCode.S},
            {"right",KeyCode.D},
            {"jump",KeyCode.Space},
            {"crouch",KeyCode.C},
            {"interact",KeyCode.E},
            {"pause",KeyCode.Escape},
            {"attack",KeyCode.Mouse0},
            {"charge",KeyCode.Mouse1},
            {"punch",KeyCode.V},
            {"ability",KeyCode.Q},
            {"freecam",KeyCode.Z},
            {"cycle arm",KeyCode.X}
        };
    }
    public static void resetGraphics()
    {
        gQualityInt = 3;
        FOVFlt = 80f;
    }
    public static void resetAudio()
    {
        masterVolumeFlt = 1f;
        musicVolumeFlt = 1f;
        effectsVolumeFlt = 1f;
    }
    public static void resetControls()
    {
        sensitivityFlt = 70f;
    }
}
