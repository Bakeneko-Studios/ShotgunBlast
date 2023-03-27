using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    void Awake() {instance=this;}
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
}
