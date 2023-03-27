using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRoom : Interactable
{
    public int bonus;
    public string scene;

    public override void OnInteract()
    {
        Player.ChangeBallz(bonus);
        LevelManager.instance.switchScene(scene);
    }
}
