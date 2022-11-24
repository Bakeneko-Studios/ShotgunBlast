using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static float sensitivity = 0.3f;
    [SerializeField] Transform playerCam;
    //[SerializeField] Transform hands;
    [SerializeField] float xClamp = 85f;    
    private float xRotation = 0f;
    private float mouseX;
    private float mouseY;

    void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        if(Cursor.lockState == CursorLockMode.Locked) {
            playerCam.eulerAngles = targetRotation;
            transform.Rotate(Vector3.up, mouseX);
        }
        // foreach (Transform hand in hands)
        // {
        //     hand.eulerAngles = targetRotation;
        // }
    }

    public void RecieveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensitivity;
        mouseY = mouseInput.y * sensitivity;
    }
}
