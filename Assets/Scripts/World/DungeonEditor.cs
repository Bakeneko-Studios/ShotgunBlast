#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenV3))]
public class DungeonEditor : Editor
{
    public override void OnInspectorGUI()
    {            
        DungeonGenV3 gen = (DungeonGenV3)target;
        
        GUILayout.Label("DO NOT USE IN EDITOR MODE");
        if(GUILayout.Button("Generate"))
        {
            gen.clear();
            gen.generate();
        }
        
        DrawDefaultInspector();
    }
}
#endif