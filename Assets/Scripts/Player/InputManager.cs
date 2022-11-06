using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;//Character controller
    //[SerializeField] PlayerMovementNew movement;//Janky RB 
    //[SerializeField] PlayerMovementBill movement;//Bill RB

    [SerializeField] MouseLook mouselook;
    [SerializeField] PlayerAction action;
    [SerializeField] Shotgun shotgun;
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

        daMove.HorizontalMove.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        daMove.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        daMove.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        daMove.Interact.performed += _ => action.OnEPressed();

        daMove.Jump.performed += _ => movement.OnJumpPressed();
        daMove.MouseY.performed += _ => movement.OnDashPressed();

        //*Combat
        daCombat.PrimaryFire.performed += _ => shotgun.OnPrimaryFire();
    }
    void Update()
    {
        movement.RecieveInput(horizontalInput);
        mouselook.RecieveInput(mouseInput);
    }


    //Enabling the input thing else it won't work
    private void OnEnable() 
    {
        daMove.Enable();
        daCombat.Enable();
    }
    private void OnDisable()
    {
        daMove.Disable();
        daCombat.Disable();
    }
}
