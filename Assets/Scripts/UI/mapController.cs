using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapController : MonoBehaviour
{
    public Transform MapCamera;
    public float scrollSpeed = 2.0f;
    public float dragMultiplier = 0.1f;

    void OnEnable() {
        Transform Minimap = GameObject.Find("MinimapCam").transform;
        MapCamera.position = Minimap.position;
    }


    Vector2 orginalMouse;
    Vector3 orginalCamera;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            orginalMouse = Input.mousePosition;
            orginalCamera = MapCamera.position; 
            StartCoroutine(isDown());
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && MapCamera.GetComponent<Camera>().orthographicSize < 69) {
            MapCamera.GetComponent<Camera>().orthographicSize += scrollSpeed * 1.2f; 
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && MapCamera.GetComponent<Camera>().orthographicSize > 25) {
            MapCamera.GetComponent<Camera>().orthographicSize -= scrollSpeed;
        }
    }

    IEnumerator isDown() {
        while (!Input.GetMouseButtonUp(0)) {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 delta = orginalMouse - mousePosition;
            //Debug.Log(delta);
            MapCamera.position = orginalCamera + new Vector3(delta.x * dragMultiplier,0,delta.y * dragMultiplier);
            yield return null;
        }
    }
}
