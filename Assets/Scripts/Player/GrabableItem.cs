using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableItem : MonoBehaviour
{
    public GameObject targetHand;
    public void Grab()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        transform.SetParent(targetHand.transform);
    }
    public void Drop()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        transform.SetParent(null);        
    }
}
