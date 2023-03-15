using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public string[] dialogue;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject bar;
    private int OnChat;

    movement mv;

    void OnInteract() {
        OnChat = 0;
        bar.SetActive(true);
        NextLine();
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bar);
        mv = movement.instance;
        mv.enabled = false;
    }

    public void NextLine() {
        text.text = dialogue[OnChat];
        if (OnChat < dialogue.Length) {
            OnChat ++;
        } else {
            bar.SetActive(false);
            mv.enabled = true;
        }
    }
}
