using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEditor;

public class LevelManager : MonoBehaviour
{
    public void switchScene(string SceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
        LoadingScreen.SceneToLoad = SceneName;
    }

    public void switchSceneWithPlayer(string SceneName)
    {
        GameObject Player = movement.instance.gameObject;
        // string localPath = "Assets/Prefabs/savedPlayer.prefab";
        // PrefabUtility.SaveAsPrefabAsset(Player,localPath);

        SceneManager.LoadScene("LoadingScreen");
        LoadingScreen.SceneToLoad = SceneName;
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
