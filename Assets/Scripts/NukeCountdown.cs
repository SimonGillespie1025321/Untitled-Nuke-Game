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
    public GameObject[] ledDisplay;
    public Material[] ledNumbers;
         
    public void Initialise()
    {
        nukeCountdownReachedZero += EventManager.Instance.NukeCountdownExpired;

        EventManager.startNukeCountdown += startCountdown;
        EventManager.stopNukeCountdown += stopCountdown;
        timeRemaining = GameManager.Instance.gameConfig.timerInSeconds;
        ResetCountdown();
    }


    private void ResetCountdown()
    {
        for (int index = 0; index < ledDisplay.Length; index++)
        {
            ledDisplay[index].GetComponentInChildren<Renderer>().material = ledNumbers[0];
        }
    }
    private void startCountdown()
    {
        Debug.Log("TIMER STARTED");
        timeRemaining = GameManager.Instance.gameConfig.timerInSeconds;
        isTimerRunning = true;
        DisplayCountdown();
    }

    private void stopCountdown()
    {
        Debug.Log("TIMER STOPPED");
        isTimerRunning = false;
    }



    void Update()
    {
        //Debug.Log("TIMER STARTED");
        if (isTimerRunning)
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

                string timerText = string.Format("{0:00}{1:00}", minutes, seconds);
                char[] timerNumbers = timerText.ToCharArray();
                for (int index = 0; index < ledDisplay.Length; index++)
                {
                    
                    int numberToDisplay;
                    int.TryParse(timerNumbers[index].ToString(), out numberToDisplay);
                    ledDisplay[index].GetComponentInChildren<Renderer>().material = ledNumbers[numberToDisplay];
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
