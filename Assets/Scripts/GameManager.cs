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


        //Start Microgame Loader
        MicroGameLoader.Instance.Initialise();

        // Start Audio Manager
        AudioManager.Instance.Initialise();

        offScreenDisplay.SetActive(true);
        mainCamera.transform.position = menuCameraPostition.transform.position;
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
            //mainCamera.transform.position = gameCameraPostition.transform.position;
            canvas.enabled = false;
            LoadNewMicrogame();
            startNukeCountDown();
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
        /* if (haveCollection)
         {
             Debug.Log("Loading:" + currentMicroGame.sceneName);
             if (unloadingGame)
             {
                 Debug.Log("Trying to load while unloading");
                 StartCoroutine(WaitForUnload());
             }
             LoadMicroGameScene(currentMicroGame.sceneName);
         Debug.Log("(Load)Number of scenes loaded:" + SceneManager.sceneCount);
         }
         else
         {
             Debug.Log("Micro Game Collection not found");
         }*/

        loadMicrogame();
    }

    private void UnloadCurrentMicrogame()
    {
        /*if (currentMicroGame.sceneName != null)
        {
            unloadingGame = true;
            Debug.Log("unloadingGame = " + unloadingGame);
            UnloadMicroGameScene(currentMicroGame.sceneName);
            
        Debug.Log("(Unload)Number of scenes loaded:" + SceneManager.sceneCount);

        }
        else
            Debug.Log("No microgame to unload");*/
        unloadMicrogame();
    }


    /*public void LoadMicroGameScene(string microGame)
    {
        StartCoroutine(WaitForUnload());
        if (currentMicroGame.sceneName != null) // && !isMicroGameLoaded)
        {
            

            AsyncOperation op = SceneManager.LoadSceneAsync(currentMicroGame.sceneName, LoadSceneMode.Additive);
            op.completed += (AsyncOperation result) =>
            {
                //Debug.Log("Loaded micro game at index: " + currentMicroGame.sceneIndex);
                //Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                isMicroGameLoaded = true;
                
                //offScreenDisplay.SetActive(false);
            };
        }
        else
            Debug.Log("No microgame to load");
    }

    public void UnloadMicroGameScene(string microGame)
    {
        
        if (currentMicroGame.sceneName != null) // && isMicroGameLoaded)
        {
            Debug.Log("Unloading:" + currentMicroGame.sceneName);
            //currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
            AsyncOperation op = SceneManager.UnloadSceneAsync(currentMicroGame.sceneName);
            op.completed += (AsyncOperation result) =>
            {
                //Debug.Log("Unloaded micro game at index: " + currentMicroGame.sceneIndex);
                //Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                isMicroGameLoaded = false;
                unloadingGame = false;
                Debug.Log("unloadingGame = " + unloadingGame);

                //offScreenDisplay.SetActive(true);
            };
        }
        else
            Debug.Log("No microgame to unload");
    }
*/

  
    public void WinCurrentMicroGame()
    {
        offScreenDisplay.GetComponent<MeshRenderer>().material.color = Color.green;
        offScreenDisplay.SetActive(true);
        UnloadCurrentMicrogame();
        indicators[indicatorIndex].GetComponent<MeshRenderer>().material.color = Color.green;
        indicatorIndex++;
        if (indicatorIndex >= gameConfig.microgamesToWin) //indicators.Length)
        {
            wonGame = true;
            Debug.Log("----------------WON WHOLE GAME---------------");
            EventManager.Instance.NukeHasBeenStopped();
        }
        else
        {
            StartCoroutine(WaitForUnload());
            //StopCoroutine(WaitForUnload());
            //LoadNewMicrogame();
            offScreenDisplay.SetActive(false);
        }
    }

    public void FailCurrentMicroGame()
    {
        offScreenDisplay.GetComponent<MeshRenderer>().material.color = Color.red;
        offScreenDisplay.SetActive(true);
        UnloadCurrentMicrogame();
        //StartCoroutine(WaitForUnload());
        //StopCoroutine(WaitForUnload());
        //LoadNewMicrogame();
        offScreenDisplay.SetActive(false);
        NukeExploded();
    }

    public void NukeExploded()
    {
        Debug.Log("!!!NUKE EXPLODED!!!");
    }

    public void NukeStopped()
    {
        Debug.Log("!!!NUKE STOPPED!!!");
    }


    IEnumerator WaitForUnload()
    {
        Debug.Log("waiting for unload:" + Time.deltaTime.ToString());
        //yield return new WaitWhile(() => (unloadingGame));
        yield return new WaitForSeconds(1f);
        Debug.Log("waited for unload:" + Time.deltaTime.ToString());

    }

    public void QuitGame()
    {
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
