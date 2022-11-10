using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPanelAnimations : MonoBehaviour
{
    public Animator title;
    public Animator buttons;

    void Start() {
        title.updateMode = AnimatorUpdateMode.UnscaledTime; 
        buttons.updateMode = AnimatorUpdateMode.UnscaledTime; 
    }

    public void mainExpand() {
        title.Play("maintitleup");
        buttons.Play("mainbuttonsside");
    }

    public void mainRetract() {
        title.Play("maintitlemain");
        buttons.Play("mainbuttonsmain");
    }

    public void mainRetractQuick() {
        title.Play("maintitlequick");
        buttons.Play("mainbuttonsquick");
        StartCoroutine(SetFalse());
    }

    IEnumerator SetFalse() {
        yield return new WaitForSeconds(.0f);
        this.gameObject.SetActive(false);
    }
}
