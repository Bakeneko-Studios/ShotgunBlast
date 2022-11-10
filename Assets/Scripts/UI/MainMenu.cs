using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject main;

    public void EnterScene()
    {
        SceneManager.LoadScene("Mod Catte Test");
    }
    public void EnterSettings()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }
    public void EnterMain()
    {
        settings.SetActive(false);
        main.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game should quit if built");
    }
}
