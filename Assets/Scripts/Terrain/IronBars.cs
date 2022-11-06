using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBars : MonoBehaviour
{
    [SerializeField] private float raiseHeight = 6f;
    [SerializeField] private float raiseSpeed = 2.5f;
    private float orgHeight;
    public bool raiseBars = false;
    public bool lowerBars = false;

    void Start()
    {
        orgHeight = transform.position.y;
    }
    void Update()
    {
        if (transform.position.y < orgHeight+raiseHeight && raiseBars)
        {
            RaiseLocker();
        }
        if (transform.position.y > orgHeight && lowerBars)
        {
            LowerLocker();
        }
        if (transform.position.y == orgHeight+raiseHeight)
        {
            raiseBars = false;
            lowerBars = false;
        }
    }

    public void RaiseLocker()
    {
        transform.Translate(Vector3.up * Time.deltaTime * raiseSpeed, Space.World);
    }
    public void LowerLocker()
    {
        transform.Translate(Vector3.down * Time.deltaTime * raiseSpeed, Space.World);        
    }

}
