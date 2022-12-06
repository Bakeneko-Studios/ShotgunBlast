using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class room : MonoBehaviour
{
    // Start is called before the first frame update

    // DO STUFF WHEN ENTER ROOM
    // open and close gate when player enters
    // play music for level and other room effects

    public GameObject text;
    public GameObject gate;
    public GameObject[] enemies;
    private Collider colide;
    bool roomEntered = false;
    

    void Start()
    {
        colide = gate.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void enterRoomEffects()
    {
        roomEntered = true;
        colide.isTrigger = false;
        gate.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Player" && !roomEntered)
            enterRoomEffects();
    }
}
