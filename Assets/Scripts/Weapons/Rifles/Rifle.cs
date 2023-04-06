using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rifle : Gun
{
    public bool ADS;
    [SerializeField] protected Vector3 ADSPos;
    public float ADSSmoothing;
}
