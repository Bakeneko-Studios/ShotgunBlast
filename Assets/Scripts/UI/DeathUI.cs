using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public static DeathUI instance;
    public GameObject deathPanel;
    [SerializeField] private GameObject defaultSelected;
    [SerializeField] private TMP_Text[] buttonTexts;
    [SerializeField] private TMPro.TMP_FontAsset Glow;
    [SerializeField] private TMPro.TMP_FontAsset noGlow;


    void Start() {
        instance = this;
    }

    public void deathEvent() {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(defaultSelected, new BaseEventData(eventSystem));
    }

    public void UpdateGlow(TMP_Text text) {
        foreach(var t in buttonTexts) {
            if (t == text) { //trade materials
                t.font = Glow;
            } else {
                t.font = noGlow;
            }
        }
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToShop(LevelManager lm) {
        Time.timeScale = 1;
        lm.switchScene("RestingGround");
    }
}
