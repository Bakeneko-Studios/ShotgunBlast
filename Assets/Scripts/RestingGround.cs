using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RestingGround : MonoBehaviour
{
    public Transform SpawnHere;

    void Awake() {
        GameObject prefabPlayer = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/savedPlayer.prefab");
        GameObject player = Instantiate(prefabPlayer);
        player.transform.position = SpawnHere.position;
    }
}
