using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicroGameLoader : Singleton<MicroGameLoader>
{

    public MicroGameDefinition currentMicroGame;

    // Start is called before the first frame update
    public void Initialise()
    {
        Debug.Log("INIT Microgame Loader");
        EventManager.loadNewMicroGame += LoadMicrogame;
        EventManager.unloadCurrentMicroGame += UnloadMicrogame;
    }

  

    public void LoadMicrogame()
    {
        //Debug.Log("MICROGAME LOADER: " + currentMicroGame.name);
        if (currentMicroGame.sceneName != null) // && !isMicroGameLoaded)
        {


            AsyncOperation op = SceneManager.LoadSceneAsync(currentMicroGame.sceneName, LoadSceneMode.Additive);
            op.completed += (AsyncOperation result) =>
            {
                //Debug.Log("Loaded micro game at index: " + currentMicroGame.sceneIndex);
                //Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                //isMicroGameLoaded = true;

                //offScreenDisplay.SetActive(false);
            };
        }
        else
            Debug.Log("No microgame to load");
    }

    public void UnloadMicrogame()
    {
        //Debug.Log("MICROGAME UNLOADER: " + currentMicroGame.name);
        if (SceneManager.sceneCount > 1)
            if (currentMicroGame.sceneName != null) // && isMicroGameLoaded)
            {
                //Debug.Log("Unloading:" + currentMicroGame.sceneName);
                //Debug.Log("scenecount" + SceneManager.sceneCount);
                //currentMicroGame.sceneIndex = SceneManager.GetSceneByName(currentMicroGame.sceneName).buildIndex;
                try
                {
                    AsyncOperation op = SceneManager.UnloadSceneAsync(currentMicroGame.sceneName);
                    op.completed += (AsyncOperation result) =>
                    {
                    //Debug.Log("Unloaded micro game at index: " + currentMicroGame.sceneIndex);
                    //Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                    //isMicroGameLoaded = false;
                    //unloadingGame = false;
                    Debug.Log("unloadingGame = " + currentMicroGame.sceneName);

                    //offScreenDisplay.SetActive(true);
                };
                }
                catch (Exception e)
                {
                    //Debug.Log("Unloaded micro game at index: " + currentMicroGame.sceneIndex);
                    //Debug.Log("Number of scenes loaded:" + SceneManager.sceneCount);
                    //isMicroGameLoaded = false;
                    //unloadingGame = false;
                    //Debug.Log("unloadingGame = " + currentMicroGame.sceneName);

                };
            }
            else {
            //Debug.Log("No microgame to unload");
            }

    }
}
