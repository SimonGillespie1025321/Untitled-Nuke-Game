using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class GameManager : Singleton<GameManager>
{
    private int loadedMicroGameType;
    private int loadedSceneIndex;
    private bool haveCollection = false;
    private bool unloadingGame = false;
    private bool isPlaying = false;

    public MicroGameCollection microGameCollection;
    public MicroGameDefinition currentMicroGame;
    private int currentMicroGameIndex;

    private bool isMicroGameLoaded = false;
    private bool wonGame = false;
    private bool loseGame = false;


    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject offScreenDisplay;
    [SerializeField] public GameObject[] indicators;
    private int indicatorIndex = 0;
    [SerializeField] public GameObject menuCameraPostition;

    [SerializeField] public GameObject gameCameraPostition;
    [SerializeField] public GameObject mainCamera;
    [SerializeField] public AudioSource audioSource;

    [SerializeField] public MainGameConfiguration gameConfig;


    public delegate void StartNukeCountDown();
    public static event StartNukeCountDown startNukeCountDown;
    public delegate void StopNukeCountDown();
    public static event StopNukeCountDown stopNukeCountDown;
    public delegate void LoadMicrogame();
    public static event LoadMicrogame loadMicrogame;
    public delegate void UnloadMicrogame();
    public static event UnloadMicrogame unloadMicrogame;






    void Start()
    {
        Debug.Log("Initialise:" + this.name);

        // Start Event Manager
        EventManager.Instance.Initialise();

        //Start Input Manager
        PlayerController.Instance.Initialise();

        NukeCountdown.Instance.Initialise();

        //Start Microgame Loader
        MicroGameLoader.Instance.Initialise();

        // Start Audio Manager
        AudioManager.Instance.Initialise();



        offScreenDisplay.SetActive(true);
        mainCamera.transform.position = menuCameraPostition.transform.position;
        mainCamera.transform.rotation = menuCameraPostition.transform.rotation;
        canvas.enabled = true;

        EventManager.failMicroGame += FailCurrentMicroGame;
        EventManager.winMicroGame += WinCurrentMicroGame;
        EventManager.nukeExploded += NukeExploded;
        EventManager.nukeStopped += NukeStopped;
        loadMicrogame += EventManager.Instance.LoadMicroGame;
        unloadMicrogame += EventManager.Instance.UnloadCurrentMicroGame;
        startNukeCountDown += EventManager.Instance.StartCountDown;
        stopNukeCountDown += EventManager.Instance.StopCountDown;

        GetMicroGameCollection();

        GameMenu();

    }

    private void GameMenu()
    {
        Debug.Log("IN GAME MENU");
        mainCamera.transform.position = menuCameraPostition.transform.position;
        mainCamera.transform.rotation = menuCameraPostition.transform.rotation;
        audioSource.Stop();
        isPlaying = false;
        for(int resetIndicators = 0; resetIndicators < indicators.Length; resetIndicators++ )
        {
            indicators[resetIndicators].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        indicatorIndex = 0;
        canvas.enabled = true;
        isPlaying = false;
        EventManager.tap += TapStart;

    }

    private void TapStart()
    {
        if (!isPlaying)
        {
            //EventManager.tap -= TapStart;
            Debug.Log("TAP START ISPLAYING");
            isPlaying = true;
            offScreenDisplay.SetActive(false);
            canvas.enabled = false;
            LoadNewMicrogame();
            mainCamera.transform.position = gameCameraPostition.transform.position;
            mainCamera.transform.rotation = gameCameraPostition.transform.rotation;
            startNukeCountDown();
            audioSource.Play();
        }
                      

    }

   
    
    private void GetMicroGameCollection()
    {
        if (TryGetComponent<MicroGameCollection>(out microGameCollection))
        {
            Debug.Log("FOUND GAME COLLECTION:");
            if (microGameCollection.microGameDefinition.Length != 0)
            {
                currentMicroGameIndex = microGameCollection.microGameDefinition.Length;
                currentMicroGame = microGameCollection.microGameDefinition[currentMicroGameIndex - 1];
                currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
                haveCollection = true;
            }
        }
        else Debug.Log("COULDN'T FIND GAME COLLECTION");
    }


    private void LoadNewMicrogame()
    {
        
        loadMicrogame();
    }

    private void UnloadCurrentMicrogame()
    {
        
        unloadMicrogame();
    }


    
  
    public void WinCurrentMicroGame()
    {
        //offScreenDisplay.GetComponent<MeshRenderer>().material.color = Color.green;
        offScreenDisplay.SetActive(true);
        
        UnloadCurrentMicrogame();
        indicators[indicatorIndex].GetComponent<MeshRenderer>().material.color = Color.green;
        indicatorIndex++;
        if (indicatorIndex >= gameConfig.microgamesToWin) //indicators.Length)
        {
            wonGame = true;
            Debug.Log("----------------WON WHOLE GAME---------------");
            indicatorIndex = 0;
            mainCamera.transform.position = menuCameraPostition.transform.position;
            mainCamera.transform.rotation = menuCameraPostition.transform.rotation;
            EventManager.Instance.NukeHasBeenStopped();
        }
        else
        {
            if (!wonGame)
            {
                LoadNewMicrogame();
                offScreenDisplay.SetActive(false);
            }
        }
    }



    public void FailCurrentMicroGame()
    {
        //offScreenDisplay.GetComponent<MeshRenderer>().material.color = Color.red;
        offScreenDisplay.SetActive(true);
        UnloadCurrentMicrogame();
        if (NukeCountdown.Instance.isTimerRunning)
        {
            if (!wonGame)
            {
                LoadNewMicrogame();
            }
                offScreenDisplay.SetActive(false);
        }
    }

    public void NukeExploded()
    {
        Debug.Log("!!!NUKE EXPLODED!!!");
        canvas.enabled = true;
        UnloadCurrentMicrogame();
        GameMenu();
    }

    public void NukeStopped()
    {
        Debug.Log("!!!NUKE STOPPED!!!");
        canvas.enabled =true;
        UnloadCurrentMicrogame();
        GameMenu();
    }


    IEnumerator WaitForUnload()
    {
        Debug.Log("waiting for unload:" + Time.deltaTime.ToString());
        //yield return new WaitWhile(() => (unloadingGame));
        yield return new WaitForSeconds(2f);
        Debug.Log("waited for unload:" + Time.deltaTime.ToString());

    }

    public void QuitGame()
    {
        Debug.Log("QUIT PRESSED");
        UnloadCurrentMicrogame();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif

    }

    public void KillAllManagers()
    {
        PlayerController.Instance.DestroyInstance();
        AudioManager.Instance.DestroyInstance();
        EventManager.Instance.DestroyInstance();
    }


}
