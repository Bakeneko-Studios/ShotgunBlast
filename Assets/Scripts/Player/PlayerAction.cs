using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Camera cam;
    UnityEvent onInteract;
    public GameObject hint;
    private float interactRange = 5f;
    private bool eIsPressed = false;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange))
        {
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                hint.SetActive(true);
                onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (eIsPressed)
                {
                    onInteract.Invoke();
                    eIsPressed = false;
                    hint.SetActive(false);
                }
            }
            else
            {
                hint.SetActive(false);
            }
        }
        
    }
    public void OnEPressed()
    {
        eIsPressed = true;
    }
}
