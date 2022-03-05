using System;
using UnityEngine;
using UnityEngine.InputSystem;

public  class PlayerController : Singleton<PlayerController>
{
    public NukeInputActions nukeInputActions;

    
    private void OnEnable()
    {
        nukeInputActions = new NukeInputActions();
    }

    public void Initialise()
    {
        nukeInputActions.Player.Tap.performed += EventManager.Instance.KeyTap;
        nukeInputActions.Player.Tap.Enable();
        nukeInputActions.Player.TapHold.started += EventManager.Instance.KeyHold;
        nukeInputActions.Player.TapHold.canceled += EventManager.Instance.KeyHoldRelease;
        nukeInputActions.Player.TapHold.Enable();
        nukeInputActions.Player.Mash.performed += EventManager.Instance.KeyMash;
        nukeInputActions.Player.Mash.Enable();

    }

    public void OnDisable()
    {
        nukeInputActions.Player.Tap.Disable();
        nukeInputActions.Player.TapHold.Disable();
        nukeInputActions.Player.Mash.Disable();

    }

    public void UnsubscribeEventManager()
    {
        nukeInputActions.Player.Tap.performed -= EventManager.Instance.KeyTap;
        nukeInputActions.Player.TapHold.started -= EventManager.Instance.KeyHold;
        nukeInputActions.Player.TapHold.canceled -= EventManager.Instance.KeyHoldRelease;
        nukeInputActions.Player.Mash.performed -= EventManager.Instance.KeyMash;
    }

}
