using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public abstract class MicroGame : MonoBehaviour
{
    private Coroutine MicroGameCoroutine;
    private WaitForSeconds defaultYieldWait = null;

    public Utility.MicroGameType microGameType;

    public string sceneName;
    public int sceneIndex;

    public int gameWinState;
    public bool isPlaying;

    [SerializeField] AudioClip[] audioAssets;



    /* this is made to be overridden with the MicroGame startup code */
    virtual public void Initialise() 
    {
        sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        Debug.Log("Scene Index: " + sceneIndex);

        
    }
  

}
