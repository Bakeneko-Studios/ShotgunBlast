using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpinny : MonoBehaviour
{
    public float spinRate;
    void Update()
    {
        transform.Rotate(0,spinRate*Time.deltaTime,0);
    }
}
