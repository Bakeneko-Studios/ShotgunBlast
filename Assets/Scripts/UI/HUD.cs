using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [Header("Weapon Display")]
    public GameObject Weapon;
    
    [Header("Healthbar Display")]
    [SerializeField] private Image hudHealthBar;
    [SerializeField] private Image hudHealthBarRolling;
    float smoothTime = 0.4f;
    // public Image menuHealthBar;
    public TextMeshProUGUI hpText;
    
    void Start() {
        instance=this;
    }

    public void ChangeHealth(float healthPercent) {
        hudHealthBar.fillAmount = healthPercent;
        StartCoroutine(SmoothDampCoroutine(healthPercent));        
    }

    private IEnumerator SmoothDampCoroutine(float healthPercent) {
        float velo = 0.0f;
        while (hudHealthBarRolling.fillAmount - healthPercent > 0.001f) {  
            float currentAmount = hudHealthBarRolling.fillAmount;
            hudHealthBarRolling.fillAmount = Mathf.SmoothDamp(currentAmount,healthPercent,ref velo,smoothTime);
            yield return null;
        }
    }
}


