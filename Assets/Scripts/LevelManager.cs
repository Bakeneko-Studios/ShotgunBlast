using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ExitLevel()
    {
        SceneManager.LoadScene("LoadingScreen");
        LoadingScreen.SceneToLoad = "RestingGround";
    }
    public void ExitRest()
    {
        SceneManager.LoadScene("LoadingScreen");
        LoadingScreen.SceneToLoad = "DemoFinal";
    }
}
