using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity;
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
        transform.Rotate(Vector3.up, mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCam.eulerAngles = targetRotation;
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
