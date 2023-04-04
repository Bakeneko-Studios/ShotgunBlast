using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelfAutomation : MonoBehaviour
{
    [SerializeField] private Transform Axis;
    [SerializeField] private Transform Piston;
    float RightLimit = -0.0093f; float xSpacing = 0.0031f;
    float TopLimit = 0.0077f; float ySpacing = -0.0052f;
    float smoothTime = 0.4f;

    public int column, row;

    void Update() {
        if(Input.GetKeyDown(KeyCode.B)) {
            PistonGoTo(column, row);
        }
    }

    public void PistonGoTo(int column, int row) {
        float x = RightLimit + (column * xSpacing);
        float y = TopLimit + (row * ySpacing);
        StartCoroutine(SmoothDampCoroutineX(x));
        StartCoroutine(SmoothDampCoroutineY(y));
    }

    void ExtendPiston() {

    }

    private IEnumerator SmoothDampCoroutineX(float TargetPos) {
        float velo = 0.0f;
        // var newPosition = new Vector3 (TargetPos,Axis.localPosition.y,Axis.localPosition.z);
        // Axis.localPosition = newPosition;
        while (Vector2.Distance(Axis.localPosition, new Vector2(TargetPos,0f)) > 0.0001f) {  
            float currentPos = Axis.localPosition.x;
            var newPosition = new Vector3 (Mathf.SmoothDamp(currentPos,TargetPos,ref velo,smoothTime),0f,Axis.localPosition.z);
            Axis.localPosition = newPosition;
            yield return null;
        }
    }

    private IEnumerator SmoothDampCoroutineY(float TargetPos) {
        float velo = 0.0f;
        // var newPosition = new Vector3 (TargetPos,Piston.localPosition.y,Piston.localPosition.z);
        // Piston.localPosition = newPosition;
        while (Vector2.Distance(Piston.localPosition, new Vector2(TargetPos,0f)) > 0.00005f) {  
            float currentPos = Piston.localPosition.x;
            var newPosition = new Vector3 (Mathf.SmoothDamp(currentPos,TargetPos,ref velo,smoothTime),0f,Piston.localPosition.z);
            Piston.localPosition = newPosition;
             yield return null;
        }
    }

}
