using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public string[] dialogue;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject bar;
    public int OnChat;

    movement mv;

    public void Talk() {
        OnChat = 0;
        bar.SetActive(true);
        NextLine();
        //StartCoroutine(AutoLine());
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(bar.transform.GetChild(0).gameObject);
        mv = movement.instance;
        mv.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextLine() {
        if (OnChat < dialogue.Length) {
            text.text = dialogue[OnChat];
            OnChat ++;
        } else {
            bar.SetActive(false);
            mv.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    IEnumerator AutoLine() {
        yield return Running();
        bar.SetActive(false);
        mv.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator Running() {
        while (OnChat < dialogue.Length) {
            text.text = dialogue[OnChat];
            OnChat++;
            yield return new WaitForSeconds(3);
        }
    }
}
