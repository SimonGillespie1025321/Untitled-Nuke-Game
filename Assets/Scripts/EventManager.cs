using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class EventManager : Singleton<EventManager>
{

    //public bool isMicroGameLoaded = true;

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



    public delegate void WinMicroGame();
    public static event WinMicroGame winMicroGame;

    public delegate void FailMicroGame();
    public static event FailMicroGame failMicroGame;

    public delegate void StartNukeCountdown();
    public static event StartNukeCountdown startNukeCountdown;

    public delegate void StopNukeCountdown();
    public static event StopNukeCountdown stopNukeCountdown;

    public delegate void NukeExploded();
    public static event NukeExploded nukeExploded;

    public delegate void NukeStopped();
    public static event NukeStopped nukeStopped;

    public delegate void LoadNewMicroGame();
    public static event LoadNewMicroGame loadNewMicroGame;

    public delegate void UnLoadCurrentMicroGame();
    public static event UnLoadCurrentMicroGame unloadCurrentMicroGame;




    public void Initialise()
    {
        Debug.Log("Initialise:" + this.name);
    }

    public void KeyTap(InputAction.CallbackContext obj)
    {
            Debug.Log("Tapped");
            tap();

    }
    public void KeyHold(InputAction.CallbackContext obj)
    {
            Debug.Log("Hold");
            hold();
    }

    public void KeyHoldRelease(InputAction.CallbackContext obj)
    {
            Debug.Log("Released");
            release();
    }

    public void KeyMash(InputAction.CallbackContext obj)
    {
            Debug.Log("Mashed");
            mash();
    }
    



    public void Win()
    {
            Debug.Log("!!!WIN MICROGAME!!!");
            winMicroGame();
    }

    public void Fail()
    {
            Debug.Log("!!!FAIL MICROGAME!!!");
            failMicroGame();
    }

    public void StartCountDown()
    {
        startNukeCountdown();
    }

    public void StopCountDown()
    {
        stopNukeCountdown();
    }

    public void NukeCountdownExpired()
    {
        Debug.Log("!!!NUKE COUNTDOWN EXPIRED!!!");
        stopNukeCountdown();
        nukeExploded();
    }
    public void NukeHasBeenStopped()
    {
        Debug.Log("!!!NUKE HAS BEEN STOPPED!!!");
        stopNukeCountdown();
        nukeStopped();
    }


   

public void LoadMicroGame()
    {
        Debug.Log("(EVENTMANAGER)LoadMicroGame:" + GameManager.Instance.currentMicroGame.sceneName);
        MicroGameLoader.Instance.currentMicroGame = GameManager.Instance.currentMicroGame;
        Debug.Log("(EVENTMANAGER)LoadMicroGame:" + GameManager.Instance.currentMicroGame.sceneName);

    loadNewMicroGame();

    }

    public void UnloadCurrentMicroGame()
    {
        Debug.Log("(EVENTMANAGER)UnloadMicroGame");
        unloadCurrentMicroGame();
    }


}

