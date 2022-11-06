//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""b381e123-5160-4b27-93eb-1fbc41cf63be"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9a827bac-1989-4c85-9a16-44ef2c7ffc73"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3dd4ad4d-e92c-49ab-9b0d-22c9eaf618ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8e97d9af-8dd9-4d64-84e8-514f3a7331a3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6cfa948e-d2ba-4191-9b2a-00a8e08b0d70"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8e5c7b5a-d351-429a-9a95-6de8d1930c4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b3a53908-3011-40d5-8d56-863874a782da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5921fe7-a51f-461f-94b7-d78f581aa634"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Horizontal Movement"",
                    ""id"": ""a510a368-b6ad-4271-bf5a-831f1f9d1182"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""797f6b45-7c33-4397-bc58-2b5e286e7fd6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2a6b8a3f-6e38-4851-bb2b-cfd7a78b815b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""46e56d76-a6e5-48e6-9f69-7d2609102d4b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da23a20f-773c-409d-969d-41674969b94e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2e530852-4148-41d2-8ad9-651fbbe65964"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50f83d08-915a-4866-9caf-aabbc460ceca"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7812b21-748a-4a0e-b545-137e3fa92880"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73c1c006-6e28-47ce-a323-72544ec06dee"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""1aeda99f-c999-40fd-9c68-035262dc6027"",
            ""actions"": [
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""9d100713-68fa-40ca-86e6-e12473fe3ce0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftPunch"",
                    ""type"": ""Button"",
                    ""id"": ""2d32a625-6366-4e0c-bb9f-83a69613798d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8cab5223-8c21-4be1-8749-5115f6cf8348"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27cee26f-67fe-4bcf-9369-47167d739ba5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftPunch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Move
        m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
        m_Move_HorizontalMove = m_Move.FindAction("HorizontalMove", throwIfNotFound: true);
        m_Move_Jump = m_Move.FindAction("Jump", throwIfNotFound: true);
        m_Move_MouseX = m_Move.FindAction("MouseX", throwIfNotFound: true);
        m_Move_MouseY = m_Move.FindAction("MouseY", throwIfNotFound: true);
        m_Move_Dash = m_Move.FindAction("Dash", throwIfNotFound: true);
        m_Move_Interact = m_Move.FindAction("Interact", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_PrimaryFire = m_Combat.FindAction("PrimaryFire", throwIfNotFound: true);
        m_Combat_LeftPunch = m_Combat.FindAction("LeftPunch", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Move
    private readonly InputActionMap m_Move;
    private IMoveActions m_MoveActionsCallbackInterface;
    private readonly InputAction m_Move_HorizontalMove;
    private readonly InputAction m_Move_Jump;
    private readonly InputAction m_Move_MouseX;
    private readonly InputAction m_Move_MouseY;
    private readonly InputAction m_Move_Dash;
    private readonly InputAction m_Move_Interact;
    public struct MoveActions
    {
        private @PlayerInput m_Wrapper;
        public MoveActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMove => m_Wrapper.m_Move_HorizontalMove;
        public InputAction @Jump => m_Wrapper.m_Move_Jump;
        public InputAction @MouseX => m_Wrapper.m_Move_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Move_MouseY;
        public InputAction @Dash => m_Wrapper.m_Move_Dash;
        public InputAction @Interact => m_Wrapper.m_Move_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Move; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
        public void SetCallbacks(IMoveActions instance)
        {
            if (m_Wrapper.m_MoveActionsCallbackInterface != null)
            {
                @HorizontalMove.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnHorizontalMove;
                @Jump.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnJump;
                @MouseX.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseX;
                @MouseY.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseY;
                @MouseY.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseY;
                @MouseY.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnMouseY;
                @Dash.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnDash;
                @Interact.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MoveActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMove.started += instance.OnHorizontalMove;
                @HorizontalMove.performed += instance.OnHorizontalMove;
                @HorizontalMove.canceled += instance.OnHorizontalMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
                @MouseY.started += instance.OnMouseY;
                @MouseY.performed += instance.OnMouseY;
                @MouseY.canceled += instance.OnMouseY;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MoveActions @Move => new MoveActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_PrimaryFire;
    private readonly InputAction m_Combat_LeftPunch;
    public struct CombatActions
    {
        private @PlayerInput m_Wrapper;
        public CombatActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PrimaryFire => m_Wrapper.m_Combat_PrimaryFire;
        public InputAction @LeftPunch => m_Wrapper.m_Combat_LeftPunch;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @PrimaryFire.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPrimaryFire;
                @LeftPunch.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftPunch;
                @LeftPunch.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftPunch;
                @LeftPunch.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnLeftPunch;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PrimaryFire.started += instance.OnPrimaryFire;
                @PrimaryFire.performed += instance.OnPrimaryFire;
                @PrimaryFire.canceled += instance.OnPrimaryFire;
                @LeftPunch.started += instance.OnLeftPunch;
                @LeftPunch.performed += instance.OnLeftPunch;
                @LeftPunch.canceled += instance.OnLeftPunch;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);
    public interface IMoveActions
    {
        void OnHorizontalMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnPrimaryFire(InputAction.CallbackContext context);
        void OnLeftPunch(InputAction.CallbackContext context);
    }
}
