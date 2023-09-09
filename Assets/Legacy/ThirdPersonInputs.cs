//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/ThirdPersonInputs.inputactions
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

public partial class @ThirdPersonInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ThirdPersonInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ThirdPersonInputs"",
    ""maps"": [
        {
            ""name"": ""PlayerDefault"",
            ""id"": ""95289ca8-d1ac-48ba-89c2-403568e09d2b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a84166ae-2de1-4872-a640-6c6794b754cb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""afbef6a2-6291-49a6-9589-f238d263e8ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""762879bb-a552-4bb4-af4b-0c833c01d75a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rightclick"",
                    ""type"": ""Button"",
                    ""id"": ""4b5b57c9-43db-469f-b03b-d0fb02cd09f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseScrollY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c31a33d0-17f9-4e0e-9453-490eb18ea5cf"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""83694463-4116-4714-b38f-75e1b76f4ed1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""NewFire"",
                    ""type"": ""Button"",
                    ""id"": ""5f45a946-4266-4bb9-813f-836b5d5cd21b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""293650fb-411b-4dd7-a45a-3caeead6da9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Controls"",
                    ""id"": ""59b4afa4-1f04-4f5c-bef2-4b7f8255ad58"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4989ffa1-aca6-4677-b32a-dedac640a3f7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""308a2b17-a0a2-4376-bd27-8bbbbe66c380"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""910e72e9-e3c3-4e6d-94ef-ede1541f50e6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b28a51b0-0428-4e17-9bc3-013eb2e24c6d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7437dd8e-1cca-46ab-acc9-bffa2ee025ad"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04a26016-40c0-444b-825d-947a2a3b30a9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c72b95b0-4f63-4afc-8eaa-00893d4f2e3c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""effcc347-e318-4d1d-80d0-1db6fd0bbaec"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=0.5,y=0.5)"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb441bb-e6f3-4abb-964f-b2275d3085b3"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),StickDeadzone,ScaleVector2(x=300,y=300)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""457ca3bd-e759-4715-87be-60ad65e4349c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82bba0c7-62ba-4855-bd9a-0f059cd86614"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e28e23ba-bead-4d8b-8c9e-9eb8c17f19dc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NewFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88febd90-07ac-4ff0-8090-31ab7c62cd47"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Rightclick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88c8a32b-7d39-431e-bc20-6910aa5e96ed"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""MouseScrollY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ee05797-d27b-4d9c-b414-f655644e0a0f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""380a05ce-ede4-4a35-bb76-aa22935bde53"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerDefault
        m_PlayerDefault = asset.FindActionMap("PlayerDefault", throwIfNotFound: true);
        m_PlayerDefault_Move = m_PlayerDefault.FindAction("Move", throwIfNotFound: true);
        m_PlayerDefault_Sprint = m_PlayerDefault.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerDefault_Jump = m_PlayerDefault.FindAction("Jump", throwIfNotFound: true);
        m_PlayerDefault_Rightclick = m_PlayerDefault.FindAction("Rightclick", throwIfNotFound: true);
        m_PlayerDefault_MouseScrollY = m_PlayerDefault.FindAction("MouseScrollY", throwIfNotFound: true);
        m_PlayerDefault_Look = m_PlayerDefault.FindAction("Look", throwIfNotFound: true);
        m_PlayerDefault_NewFire = m_PlayerDefault.FindAction("NewFire", throwIfNotFound: true);
        m_PlayerDefault_Test = m_PlayerDefault.FindAction("Test", throwIfNotFound: true);
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

    // PlayerDefault
    private readonly InputActionMap m_PlayerDefault;
    private List<IPlayerDefaultActions> m_PlayerDefaultActionsCallbackInterfaces = new List<IPlayerDefaultActions>();
    private readonly InputAction m_PlayerDefault_Move;
    private readonly InputAction m_PlayerDefault_Sprint;
    private readonly InputAction m_PlayerDefault_Jump;
    private readonly InputAction m_PlayerDefault_Rightclick;
    private readonly InputAction m_PlayerDefault_MouseScrollY;
    private readonly InputAction m_PlayerDefault_Look;
    private readonly InputAction m_PlayerDefault_NewFire;
    private readonly InputAction m_PlayerDefault_Test;
    public struct PlayerDefaultActions
    {
        private @ThirdPersonInputs m_Wrapper;
        public PlayerDefaultActions(@ThirdPersonInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerDefault_Move;
        public InputAction @Sprint => m_Wrapper.m_PlayerDefault_Sprint;
        public InputAction @Jump => m_Wrapper.m_PlayerDefault_Jump;
        public InputAction @Rightclick => m_Wrapper.m_PlayerDefault_Rightclick;
        public InputAction @MouseScrollY => m_Wrapper.m_PlayerDefault_MouseScrollY;
        public InputAction @Look => m_Wrapper.m_PlayerDefault_Look;
        public InputAction @NewFire => m_Wrapper.m_PlayerDefault_NewFire;
        public InputAction @Test => m_Wrapper.m_PlayerDefault_Test;
        public InputActionMap Get() { return m_Wrapper.m_PlayerDefault; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerDefaultActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerDefaultActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Rightclick.started += instance.OnRightclick;
            @Rightclick.performed += instance.OnRightclick;
            @Rightclick.canceled += instance.OnRightclick;
            @MouseScrollY.started += instance.OnMouseScrollY;
            @MouseScrollY.performed += instance.OnMouseScrollY;
            @MouseScrollY.canceled += instance.OnMouseScrollY;
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @NewFire.started += instance.OnNewFire;
            @NewFire.performed += instance.OnNewFire;
            @NewFire.canceled += instance.OnNewFire;
            @Test.started += instance.OnTest;
            @Test.performed += instance.OnTest;
            @Test.canceled += instance.OnTest;
        }

        private void UnregisterCallbacks(IPlayerDefaultActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Rightclick.started -= instance.OnRightclick;
            @Rightclick.performed -= instance.OnRightclick;
            @Rightclick.canceled -= instance.OnRightclick;
            @MouseScrollY.started -= instance.OnMouseScrollY;
            @MouseScrollY.performed -= instance.OnMouseScrollY;
            @MouseScrollY.canceled -= instance.OnMouseScrollY;
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @NewFire.started -= instance.OnNewFire;
            @NewFire.performed -= instance.OnNewFire;
            @NewFire.canceled -= instance.OnNewFire;
            @Test.started -= instance.OnTest;
            @Test.performed -= instance.OnTest;
            @Test.canceled -= instance.OnTest;
        }

        public void RemoveCallbacks(IPlayerDefaultActions instance)
        {
            if (m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerDefaultActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerDefaultActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerDefaultActions @PlayerDefault => new PlayerDefaultActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerDefaultActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRightclick(InputAction.CallbackContext context);
        void OnMouseScrollY(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnNewFire(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
    }
}