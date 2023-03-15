using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public string[] dialogue;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject bar;
    private int OnChat;

    void OnInteract() {
        OnChat = 0;
        bar.SetActive(true);
        NextLine();
    }

    public void NextLine() {
        text.text = dialogue[OnChat];
        if (OnChat < dialogue.Length) {
            OnChat ++;
        } else {
            bar.SetActive(false);
        }
    }
}
