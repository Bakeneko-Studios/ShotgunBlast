using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [Header("Weapon Display")]
    public Image Weapon;
    
    [Header("Healthbar Display")]
    public Image hudHealthBar;
    // public Image menuHealthBar;
    public TextMeshProUGUI hpText;
    
    void Start() {
        instance=this;
    }

}


