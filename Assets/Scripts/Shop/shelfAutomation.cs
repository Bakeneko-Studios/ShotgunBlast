using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shelfAutomation : MonoBehaviour
{
    [SerializeField] private Animation MoveBox;
    [SerializeField] private AnimationCurve AnimClip;
    float RightLimit = -0.0093f; float xSpacing = 0.0031f;
    float TopLimit = 0.0077f; float ySpacing = -0.0052f;
    public 
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
        //float curveValue = AnimClip.Evaluate();
    }

    void ExtendPiston() {

    }

}
