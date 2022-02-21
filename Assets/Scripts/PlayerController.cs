using System;
using UnityEngine;
using UnityEngine.InputSystem;

public  class PlayerController : Singleton<PlayerController>
{
    public NukeInputActions nukeInputActions;
   /* private InputAction tapKey;
    private InputAction holdKey;
    private InputAction mashKey;*/

    public delegate void NukeKeyPressed();
    public static event NukeKeyPressed nukeKeyPressed;

    // Start is called before the first frame update
    public void Initialise()
    {
        

    }

    // Update is called once per frame
    private void OnEnable()
    {
        nukeInputActions = new NukeInputActions();
    }

    public void SetKeyFunction(Utility.MicroGameType gameType)
    {
        switch (gameType)
        {
            case Utility.MicroGameType.Tap:
                {
                    nukeInputActions.Player.Tap.performed += KeyTap;
                    nukeInputActions.Player.Tap.Enable();
                    nukeInputActions.Player.TapHold.Disable();
                    nukeInputActions.Player.Mash.Disable();
                    break; }
            case Utility.MicroGameType.Hold:
                {
                    nukeInputActions.Player.TapHold.started += KeyHold;
                    nukeInputActions.Player.TapHold.canceled += KeyHoldRelease;
                    nukeInputActions.Player.TapHold.Enable();
                    break; }
            case Utility.MicroGameType.Mash:
                {
                    nukeInputActions.Player.Mash.performed += KeyMash;
                    nukeInputActions.Player.Mash.Enable();
                    
                    break; }
            case Utility.MicroGameType.Rhythm:
                {
                    break; }

        }

    }

    private void KeyTap(InputAction.CallbackContext obj)
    {
        Debug.Log("Tapped");
        nukeKeyPressed();

    }
    private void KeyHold(InputAction.CallbackContext obj)
    {
        Debug.Log("Held");
        nukeKeyPressed();
    }

    private void KeyHoldRelease(InputAction.CallbackContext obj)
    {
        Debug.Log("Released");
        nukeKeyPressed();
    }

    private void KeyMash(InputAction.CallbackContext obj)
    {
        Debug.Log("Mashed");
        nukeKeyPressed();
    }

    
    private void OnDestroy()
    {
        nukeInputActions.Player.Tap.performed -= KeyTap;

        nukeInputActions.Player.TapHold.started -= KeyHold;
        nukeInputActions.Player.TapHold.canceled -= KeyHoldRelease;

        nukeInputActions.Player.Mash.performed -= KeyMash;
    }


}
