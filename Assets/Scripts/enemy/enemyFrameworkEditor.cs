#if (UNITY_EDITOR)
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(enemyFramework))]
public class enemyFrameworkEditor : Editor
{
    // Start is called before the first frame update
    private void OnSceneGUI()
    {
        enemyFramework eF = (enemyFramework)target;
        //Handles.color = Color.yellow;
        //Handles.DrawWireArc(eF.transform.position, Vector3.up, Vector3.forward, 360, eF.sightRange);

        Vector3 viewAngle01 = DirectionFromAngle(eF.transform.eulerAngles.y, -eF.viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(eF.transform.eulerAngles.y, eF.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(eF.transform.position, eF.transform.position + viewAngle01 * eF.viewAngle);
        Handles.DrawLine(eF.transform.position, eF.transform.position + viewAngle02 * eF.viewAngle);
            
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
#endif