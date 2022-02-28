using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    Scene microGame;
    public int loadedMicroGameType;
    public int loadedSceneIndex;
    public Utility.MicroGameType loadedGameType;

    // Start is called before the first frame update
    void Start()
    {
        // Start Event Manager
        //EventManager.Instance.Initialise();

        // Start Audio Manager
        AudioManager.Instance.Initialise();

        PlayerController.Instance.Initialise();

        //LoadMicrogame("ParachuteJump");

    }


    public void LoadMicrogame(string microGame)
    {
            GameObject.Find("ScreenOff")?.SetActive(false);

            AsyncOperation op = SceneManager.LoadSceneAsync(microGame, LoadSceneMode.Additive);
            op.completed += (AsyncOperation result) =>
            {
                Debug.Log("MicroGame scene: ");

            };
    }

}
