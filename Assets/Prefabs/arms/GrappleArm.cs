using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleArm : MonoBehaviour
{
    public float speed = 20f; // Speed at which the player is pulled towards the hook
    public LineRenderer line;
    private Vector3 hookTarget;
    private bool isHooked;
    public Transform cam;
    public GameObject leftHand;
    public GameObject Player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetHookTarget();
        }
        if (isHooked)
        {
            PullTowardsHook();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ReleaseHook();
        }
    }

    void SetHookTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            leftHand.transform.LookAt(hit.point);
            hookTarget = hit.point;
            isHooked = true;
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookTarget);
        }
    }

    void PullTowardsHook()
    {
        Vector3 hookDirection = (hookTarget - transform.position).normalized;
        Player.GetComponent<Rigidbody>().velocity = hookDirection * speed;
        line.SetPosition(0, transform.position);
    }

    void ReleaseHook()
    {
        isHooked = false;
        line.enabled = false;
    }
}

