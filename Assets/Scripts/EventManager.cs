using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventManager : Singleton<EventManager>
{

    public bool isMicroGameLoaded = true;
    //Input events
    public delegate void Tap();
    public static event Tap tap; 
    public delegate void TapHold();
    public static event TapHold hold;
    public delegate void TapHoldRelease();
    public static event TapHoldRelease release;
    public delegate void Mash();
    public static event Mash mash;

    //Gamestate events

    public delegate void NewMicroGameLoaded();
    public static event NewMicroGameLoaded newMicroGameLoaded;

    public delegate void WinMicroGame();
    public static event WinMicroGame winMicroGame;

    public delegate void FailMicroGame();
    public static event FailMicroGame failMicroGame;


    public void Initialise()
    {
    }

    public void KeyTap(InputAction.CallbackContext obj)
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("Tapped");
            tap();
        }

    }
    public void KeyHold(InputAction.CallbackContext obj)
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("Hold");
            hold();
        }
    }

    public void KeyHoldRelease(InputAction.CallbackContext obj)
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("Released");
            release();
        }
    }

    public void KeyMash(InputAction.CallbackContext obj)
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("Mashed");
            mash();
        }
    }



    public void Win()
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("!!!WIN GAME!!!");
            winMicroGame();
        }
    }

    public void Fail()
    {
        if (isMicroGameLoaded)
        {
            Debug.Log("!!!FAIL GAME!!!");
            failMicroGame();
        }
    }

    public void MicroGameLoaded()
    {

    }

   


}

