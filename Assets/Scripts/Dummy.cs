using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int HitPoints = 100;
    public void minusHP(int dmg)
    {
        HitPoints -= dmg;
    }
    void Update() 
    {
        if (HitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
