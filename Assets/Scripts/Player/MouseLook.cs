using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;
    public float sensitivity;
    [SerializeField] Transform playerCam;
    //[SerializeField] Transform hands;
    [SerializeField] float xClamp = 85f;    
    private float xRotation = 0f;
    private float mouseX;
    private float mouseY;

    void Awake() 
    {
        instance=this;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        if(Cursor.lockState == CursorLockMode.Locked) {
            playerCam.eulerAngles = targetRotation;
            transform.Rotate(Vector3.up, mouseX);
        }
    }
}
