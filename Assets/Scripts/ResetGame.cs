using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    private void Update()
    {
        // Quick and dirty one-liner to reload the scene (eg use at the end of the battle)
        if( Input.GetKeyDown( KeyCode.Escape ) == true ){ QuitGame(); }
        if (Input.GetKeyDown(KeyCode.C) == true) { UnityEngine.SceneManagement.SceneManager.LoadScene(1); }
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
}
