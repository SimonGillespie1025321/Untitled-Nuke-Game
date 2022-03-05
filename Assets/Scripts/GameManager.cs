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

    private MicroGameCollection microGameCollection;
    private MicroGameDefinition currentMicroGame;
    private int currentMicroGameIndex;

    private bool isMicroGameLoaded = false;


    [SerializeField] public Canvas canvas;
    [SerializeField] public GameObject offScreenDisplay;




    void Start()
    {

        // Start Event Manager
        EventManager.Instance.Initialise();

        // Start Audio Manager
        AudioManager.Instance.Initialise();

        //Start Input Manager
        PlayerController.Instance.Initialise();

        offScreenDisplay.SetActive(true);

        EventManager.failMicroGame += FailCurrentMicroGame;
        EventManager.winMicroGame += WinCurrentMicroGame;

        GetMicroGameCollection();

        GameMenu();

    }

    private void GameMenu()
    {
        EventManager.tap += TapStart;
        //EventManager.mash += MashQuit;

    }

    private void TapStart()
    {
        EventManager.tap -= TapStart;
        canvas.enabled = false;
        LoadMicrogame();
        

    }

    private void MashQuit()
    {
        EventManager.tap -= TapStart;
        EventManager.mash -= MashQuit;
        KillAllManagers();
        QuitGame();
    }

    private void LoadMicrogame()
    {
        Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
        if (haveCollection)
        {
            currentMicroGameIndex = microGameCollection.microGameDefinition.Length;
            currentMicroGame = microGameCollection.microGameDefinition[currentMicroGameIndex-1];
            
            LoadMicroGameScene(currentMicroGame.sceneName);
        }
        else
        {
            Debug.Log("Micro Game Collection not found");
        }
    }

    private void UnloadMicroGame()
    {
        if (currentMicroGame.sceneName != null)
         UnloadMicroGameScene(currentMicroGame.sceneName);
        else
            Debug.Log("No microgame to unload");
    }

    private void GetMicroGameCollection()
    {
        if (TryGetComponent<MicroGameCollection>(out microGameCollection))
        {
            Debug.Log("FOUND GAME COLLECTION:");
            if (microGameCollection.microGameDefinition.Length != 0)
            {
                haveCollection = true;
            }
        }
        else Debug.Log("COULDN'T FIND GAME COLLECTION");
    }

    public void LoadMicroGameScene(string microGame)
    {

        if (currentMicroGame.sceneName != null)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(microGame, LoadSceneMode.Additive);
            op.completed += (AsyncOperation result) =>
            {
                currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
                Debug.Log("Loaded micro game at index: " + currentMicroGame.sceneIndex);
                Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                isMicroGameLoaded = true;
                offScreenDisplay.SetActive(false);
            };
        }
        else
            Debug.Log("No microgame to load");
    }

    public void UnloadMicroGameScene(string microGame)
    {
        if (currentMicroGame.sceneName != null)
        {
            AsyncOperation op = SceneManager.UnloadSceneAsync(microGame);
            op.completed += (AsyncOperation result) =>
            {
                currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
                Debug.Log("Unloaded micro game at index: " + currentMicroGame.sceneIndex);
                Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                isMicroGameLoaded = false;
                offScreenDisplay.SetActive(true);
            };
        }
        else
            Debug.Log("No microgame to unload");
    }


  
    public void WinCurrentMicroGame()
    {
        UnloadMicroGame();
    }

    public void FailCurrentMicroGame()
    {
        UnloadMicroGame();

    }


    public void QuitGame()
    {
        UnloadMicroGame();

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
