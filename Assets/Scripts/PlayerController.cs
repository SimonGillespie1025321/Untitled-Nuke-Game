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
        Debug.Log("Initialise:" + this.name);

        //event can't have nothing to call so this takes care of it
        /*nukeInputActions.Player.Tap.started += DiscardPress;
        nukeInputActions.Player.Tap.performed += DiscardPress;
        nukeInputActions.Player.Tap.canceled += DiscardPress;
        nukeInputActions.Player.TapHold.started += DiscardPress;
        nukeInputActions.Player.TapHold.performed += DiscardPress;
        nukeInputActions.Player.TapHold.canceled += DiscardPress;    // issue with release to be investigated
        nukeInputActions.Player.Mash.started += DiscardPress;
        nukeInputActions.Player.Mash.performed += DiscardPress;
        nukeInputActions.Player.Mash.canceled += DiscardPress;*/

        nukeInputActions.Player.Tap.performed += EventManager.Instance.KeyTap;
        nukeInputActions.Player.Tap.Enable();
        //nukeInputActions.Player.QuitGame.Enabled();
        /*nukeInputActions.Player.TapHold.performed += EventManager.Instance.KeyHold;
        nukeInputActions.Player.TapHold.canceled += EventManager.Instance.KeyHoldRelease;
        nukeInputActions.Player.TapHold.Enable();
        nukeInputActions.Player.Mash.performed += EventManager.Instance.KeyMash;
        nukeInputActions.Player.Mash.Enable();*/

    }


    public void DiscardPress(InputAction.CallbackContext obj)
    {
        Debug.Log("Discarding press:");
    }

    public void OnDisable()
    {
        nukeInputActions.Player.Tap.Disable();
        /*nukeInputActions.Player.TapHold.Disable();
        nukeInputActions.Player.Mash.Disable();*/

    }

    public void UnsubscribeEventManager()
    {
        nukeInputActions.Player.Tap.performed -= EventManager.Instance.KeyTap;
        nukeInputActions.Player.Tap.Disable();
        /*nukeInputActions.Player.TapHold.performed -= EventManager.Instance.KeyHold;
        nukeInputActions.Player.TapHold.canceled -= EventManager.Instance.KeyHoldRelease;
        nukeInputActions.Player.Mash.performed -= EventManager.Instance.KeyMash;*/
    }

    private void OnDestroy()
    {
       // UnsubscribeEventManager();
    }

}
