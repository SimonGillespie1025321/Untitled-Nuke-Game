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



    [SerializeField] public GameObject offScreenDisplay;




    void Start()
    {

        // Start Event Manager
        EventManager.Instance.Initialise();

        // Start Audio Manager
        AudioManager.Instance.Initialise();

        //Start Input Manager
        PlayerController.Instance.Initialise();

        offScreenDisplay.SetActive(false);

        EventManager.failMicroGame += FailCurrentMicroGame;
        EventManager.winMicroGame += WinCurrentMicroGame;

        GetMicroGameCollection();

        LoadMicrogame();

        //StartCurrentMicroGame();




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
        UnloadMicroGameScene(currentMicroGame.sceneName);
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
            

            AsyncOperation op = SceneManager.LoadSceneAsync(microGame, LoadSceneMode.Additive);
            op.completed += (AsyncOperation result) =>
            {
                currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
                Debug.Log("Loaded micro game at index: " + currentMicroGame.sceneIndex);
                Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                isMicroGameLoaded = true;
            };
    }

    public void UnloadMicroGameScene(string microGame)
    {
        //StopCurrentMicroGame();
        AsyncOperation op = SceneManager.UnloadSceneAsync(microGame);
        op.completed += (AsyncOperation result) =>
        {
            currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
            Debug.Log("Unloaded micro game at index: " + currentMicroGame.sceneIndex);
            Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
            isMicroGameLoaded = false;
        };
    }


   /* public void StartCurrentMicroGame()
    {
        Debug.Log("STARTING MICROGAME: " + currentMicroGame.gameName);
        offScreenDisplay.SetActive(false);
        EventManager.Instance.StartCurrentMicroGame();
    }
*/

 /*   public void StopCurrentMicroGame()
    {
        offScreenDisplay.SetActive(true);
        EventManager.Instance.StartCurrentMicroGame();
    }*/
/*
    public void PauseCurrentMicroGame()
    {
        offScreenDisplay.SetActive(false);
    }*/

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
