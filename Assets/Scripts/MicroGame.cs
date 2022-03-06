using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public abstract class MicroGame : MonoBehaviour
{
    public Utility.MicroGameType microGameType;

    public string sceneName;
    public int sceneIndex;

    public int gameWinState;
    public bool isPlaying;
    public bool isPaused;

    [SerializeField] AudioClip[] audioAssets;

    public delegate void WinEvent();
    public static event WinEvent win;

    public delegate void FailEvent();
    public static event FailEvent fail;


    /* Initialise is made to be overridden with the MicroGame startup code 
        override must call the base
     */
    virtual public void Initialise() 
    {
        
        EventManager.tap += Tap;
        //EventManager.hold += Hold;
        //EventManager.release += Release;
        //EventManager.mash += Mash;
        win += EventManager.Instance.Win;
        fail += EventManager.Instance.Fail;
        isPlaying = true;
    }


    public void UnsubscribeEvents()
    {
        EventManager.tap -= Tap;
        EventManager.hold -= Hold;
        EventManager.release -= Release;
        EventManager.mash -= Mash;
        win -= EventManager.Instance.Win;
        fail -= EventManager.Instance.Fail;

    }


    

    virtual public void Tap()
    {
    }


    virtual public void Hold()
    {
    }

    virtual public void Release()
    {
    }

    virtual public void Mash()
    {
    }

    virtual public void Win()
    {
        win();
    }

    virtual public void Fail()
    {
        fail();
    }

    public abstract void WinConditionMet();

    public abstract void FailConditionMet();


}
