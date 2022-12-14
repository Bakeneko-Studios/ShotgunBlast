using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Scripts
    public MouseLook mouselook;
    public PlayerAction action;
    public Shotgun shotgun;
    public pauseScreen pauseScreen;
    private PlayerInput playerInput;
    private PlayerInput.MoveActions daMove;
    private PlayerInput.CombatActions daCombat;
    private Vector2 horizontalInput;
    private Vector2 mouseInput;
    void Awake() 
    {
        playerInput = new PlayerInput();
        daMove = playerInput.Move;
        daCombat = playerInput.Combat;

        daMove.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        daMove.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        daMove.Interact.performed += _ => action.OnEPressed();

        //*Combat
        daCombat.PrimaryFire.performed += _ => shotgun.OnPrimaryFire();
    }
    public void enableGun()
    {
        daCombat.Enable();
    }
    void Update()
    {
        mouselook.RecieveInput(mouseInput);
    }


    //Enabling the input thing else it won't work
    private void OnEnable() 
    {
        daMove.Enable();
        //daCombat.Enable();
    }
    private void OnDisable()
    {
        daMove.Disable();
        daCombat.Disable();
    }
}
