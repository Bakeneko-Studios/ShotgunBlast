using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartScene : MonoBehaviour
{
    public RoomMaster m;
    public void ReScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void defeatEnemy()
    {
        m.enemyRem-=1;
    }
}
