using UnityEngine;
// using UnityEditor;

public class RestingGround : MonoBehaviour
{
    public Transform SpawnHere;
    public GameObject player;

    void Awake() {
        //GameObject prefabPlayer = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/savedPlayer.prefab");
        // GameObject prefabPlayer = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
        // GameObject player = Instantiate(prefabPlayer);
        // player.transform.position = SpawnHere.position;
        GameObject.Instantiate(player,SpawnHere.position,Quaternion.identity);
    }
}
