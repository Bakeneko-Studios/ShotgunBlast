using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Camera cam;
    UnityEvent onInteract;
    public GameObject hint;
    private float interactRange = 7f;
    void Start() 
    {
        hint = GameObject.FindGameObjectWithTag("MotherLovingHint");
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange))
        {
            var a = hit.collider.GetComponent<Interactable>();
            if (a!=null)
            {
                hint.SetActive(true);
                if (Input.GetKeyDown(UserSettings.keybinds["interact"]))
                {
                    a.OnInteract();
                    hint.SetActive(false);
                }
            }
            else
            {
                hint.SetActive(false);
            }
        }
        
    }
}
