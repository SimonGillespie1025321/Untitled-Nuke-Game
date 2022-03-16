using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class NukeCountdown : Singleton<NukeCountdown>
{

    public delegate void NukeCountdownReachedZero();
    public static event NukeCountdownReachedZero nukeCountdownReachedZero;
    public float timeRemaining;
    public bool isTimerRunning = false;
    [SerializeField] public TextMeshPro timerDisplay;
         
    // Start is called before the first frame update
    public void Initialise()
    {
        nukeCountdownReachedZero += EventManager.Instance.NukeCountdownExpired;

        EventManager.startNukeCountdown += startCountdown;
        EventManager.stopNukeCountdown += stopCountdown;
        timeRemaining = GameManager.Instance.gameConfig.timerInSeconds;
    }

    private void startCountdown()
    {
        Debug.Log("TIMER STARTED");
        timeRemaining = GameManager.Instance.gameConfig.timerInSeconds;
        timerDisplay.color = Color.green;
        isTimerRunning = true;
        timerDisplay.color = Color.green;
        //DisplayCountdown();
    }

    private void stopCountdown()
    {
        Debug.Log("TIMER STOPPED");
        isTimerRunning = false;
    }

    // Update is called once per frame


    void Update()
    {
        //Debug.Log("TIMER STARTED");
        //if (isTimerRunning)
            DisplayCountdown();
    }

    private void DisplayCountdown()
    {
        if (isTimerRunning)
        {

            if (timeRemaining >= 0)
            {

                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerDisplay.text = "00:" + string.Format("{0:00}:{1:00}", minutes, seconds);
                if (seconds <= 5)
                {
                    timerDisplay.color = Color.red;

                }
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                isTimerRunning = false;
                nukeCountdownReachedZero();
                isTimerRunning = false;
                //nukeCountdownReachedZero -= EventManager.Instance.NukeCountdownExpired;
            }
        }
    }
}
