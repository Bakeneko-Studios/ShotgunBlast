using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freecam : MonoBehaviour
{
    bool fc;
    Vector3 ogPosition;
    Quaternion ogDirection;
    public int speed;
    public int sensitivity;
    float h,v;
    float mouseX,mouseY,xRotation;
    // float xlimit = 90f;
    [SerializeField] Canvas canvas;
    void Start()
    {
        
    }

    void Update()
    {
        if(!Player.paused)
        {
            if(Input.GetKeyDown(UserSettings.keybinds["freecam"]))
            {
                if(Player.state != Player.MoveState.freecam)
                {
                    Time.timeScale = 0;
                    fc=true;
                    Player.state = Player.MoveState.freecam;
                    canvas.gameObject.SetActive(false);
                    movement.instance.enabled=false;
                    MouseLook.instance.enabled=false;
                    ogPosition = transform.position;
                    ogDirection = transform.rotation;
                }
                else
                {
                    fc=false;
                    transform.position = ogPosition;
                    transform.rotation = ogDirection;
                    MouseLook.instance.enabled=true;
                    movement.instance.enabled=true;
                    canvas.gameObject.SetActive(true);
                    Time.timeScale = 1;
                }
            }
            if(fc)
            {
                h = (Input.GetKey(UserSettings.keybinds["left"])?-1:0) + (Input.GetKey(UserSettings.keybinds["right"])?1:0);
                v = (Input.GetKey(UserSettings.keybinds["back"])?-1:0) + (Input.GetKey(UserSettings.keybinds["forward"])?1:0);
                transform.Translate(new Vector3(h,0,v)*speed*Time.unscaledDeltaTime*(Input.GetKey(UserSettings.keybinds["jump"])?3:1));

                mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.unscaledDeltaTime;
                mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.unscaledDeltaTime;
                transform.rotation = Quaternion.Euler(new Vector3(mouseY,mouseX,0));
            }
        }
    }
}
