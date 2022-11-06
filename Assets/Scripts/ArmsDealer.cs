using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsDealer : MonoBehaviour
{
    public List<GameObject> ironBars;
    public void RaiseBars()
    {
        foreach (GameObject ironBar in ironBars)
        {
            IronBars x = ironBar.GetComponent<IronBars>();
            x.raiseBars = true;
        }
    }
    public void LowerBars()
    {
        foreach (GameObject ironBar in ironBars)
        {
            IronBars x = ironBar.GetComponent<IronBars>();
            x.lowerBars =true;
        }
    }
}
