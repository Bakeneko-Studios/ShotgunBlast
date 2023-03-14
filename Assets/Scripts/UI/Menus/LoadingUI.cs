using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    public string[] loadingScreenLines;

    [SerializeField] private Image loadingBar;
    [SerializeField] private TMP_Text text;
    
    void Start()
    {
        UpdateText();
        StartCoroutine(LoadingAnimation());
    }

    public void UpdateText()
    {
        text.text = loadingScreenLines[Random.Range(0,loadingScreenLines.Length)];
    }

    private IEnumerator LoadingAnimation() {
        yield return Bar();
        //Debug.Log("Nextscene");
        NextScene();
    }

    private IEnumerator Bar() {
        float t = 0.005f;
        while (loadingBar.fillAmount < 1f) {  
            loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount,1.1f,t);
            yield return null;
        }
    }

    void NextScene() {
        //Debug.Log(LoadingScreen.SceneToLoad);
        if (LoadingScreen.SceneToLoad != null) {
            SceneManager.LoadScene(LoadingScreen.SceneToLoad);
        }
    }
}
