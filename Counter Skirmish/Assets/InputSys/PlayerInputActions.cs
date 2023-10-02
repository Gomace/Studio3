//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSys/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Instance"",
            ""id"": ""b62b8480-ee8f-42dc-80a6-dee327dc7657"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""1e7ab502-28b7-482a-bbef-bab931689862"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roster"",
                    ""type"": ""Button"",
                    ""id"": ""9ea72124-05a3-4da3-903a-5e6228377458"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Q"",
                    ""type"": ""Button"",
                    ""id"": ""42c10d7a-2798-4d54-8382-87362bec8ade"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""W"",
                    ""type"": ""Button"",
                    ""id"": ""53559240-e0d0-497c-bd82-935843233897"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""E"",
                    ""type"": ""Button"",
                    ""id"": ""987f0421-58d4-4cbe-b63c-3fde44c47a11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""R"",
                    ""type"": ""Button"",
                    ""id"": ""0b68adb4-0ac6-4bea-a317-8f9fc6812823"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""e7f3840d-0ed9-455a-8fc1-b7e05937c952"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5952c205-0202-43cc-875b-b9abfd7ee804"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b713cc6-50cc-442f-9d66-4417770d74cc"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Q"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bffba76-d672-4391-be87-e8e455864ca1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""W"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c92438a-96ea-42fa-bd6d-631e0ee1a4e7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""E"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bbc2293-bea3-4d8f-bde4-f02d7e54f21d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""153b42b1-a93a-4148-a497-99a538e2ddd3"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Roster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2477bb5c-e1aa-4274-b84e-69de1a55d362"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Instance
        m_Instance = asset.FindActionMap("Instance", throwIfNotFound: true);
        m_Instance_Move = m_Instance.FindAction("Move", throwIfNotFound: true);
        m_Instance_Roster = m_Instance.FindAction("Roster", throwIfNotFound: true);
        m_Instance_Q = m_Instance.FindAction("Q", throwIfNotFound: true);
        m_Instance_W = m_Instance.FindAction("W", throwIfNotFound: true);
        m_Instance_E = m_Instance.FindAction("E", throwIfNotFound: true);
        m_Instance_R = m_Instance.FindAction("R", throwIfNotFound: true);
        m_Instance_Menu = m_Instance.FindAction("Menu", throwIfNotFound: true);
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

    // Instance
    private readonly InputActionMap m_Instance;
    private IInstanceActions m_InstanceActionsCallbackInterface;
    private readonly InputAction m_Instance_Move;
    private readonly InputAction m_Instance_Roster;
    private readonly InputAction m_Instance_Q;
    private readonly InputAction m_Instance_W;
    private readonly InputAction m_Instance_E;
    private readonly InputAction m_Instance_R;
    private readonly InputAction m_Instance_Menu;
    public struct InstanceActions
    {
        private @PlayerInputActions m_Wrapper;
        public InstanceActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Instance_Move;
        public InputAction @Roster => m_Wrapper.m_Instance_Roster;
        public InputAction @Q => m_Wrapper.m_Instance_Q;
        public InputAction @W => m_Wrapper.m_Instance_W;
        public InputAction @E => m_Wrapper.m_Instance_E;
        public InputAction @R => m_Wrapper.m_Instance_R;
        public InputAction @Menu => m_Wrapper.m_Instance_Menu;
        public InputActionMap Get() { return m_Wrapper.m_Instance; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InstanceActions set) { return set.Get(); }
        public void SetCallbacks(IInstanceActions instance)
        {
            if (m_Wrapper.m_InstanceActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMove;
                @Roster.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnRoster;
                @Roster.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnRoster;
                @Roster.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnRoster;
                @Q.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnQ;
                @Q.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnQ;
                @Q.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnQ;
                @W.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnW;
                @W.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnW;
                @W.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnW;
                @E.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnE;
                @E.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnE;
                @E.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnE;
                @R.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnR;
                @R.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnR;
                @R.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnR;
                @Menu.started -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_InstanceActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_InstanceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Roster.started += instance.OnRoster;
                @Roster.performed += instance.OnRoster;
                @Roster.canceled += instance.OnRoster;
                @Q.started += instance.OnQ;
                @Q.performed += instance.OnQ;
                @Q.canceled += instance.OnQ;
                @W.started += instance.OnW;
                @W.performed += instance.OnW;
                @W.canceled += instance.OnW;
                @E.started += instance.OnE;
                @E.performed += instance.OnE;
                @E.canceled += instance.OnE;
                @R.started += instance.OnR;
                @R.performed += instance.OnR;
                @R.canceled += instance.OnR;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public InstanceActions @Instance => new InstanceActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IInstanceActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRoster(InputAction.CallbackContext context);
        void OnQ(InputAction.CallbackContext context);
        void OnW(InputAction.CallbackContext context);
        void OnE(InputAction.CallbackContext context);
        void OnR(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}