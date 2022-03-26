using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MicroGame
{
    public bool hasPopped = false;
    public float balloonPopSize = 10f;
    [SerializeField] public GameObject balloon;


    public void Start()
    {
        Initialise();

    }

    public override void Mash()
    {
        Debug.Log("------------------------------Inflate Balloon MASH-------------------------");
        if (isPlaying)
        {
            if (!hasPopped)
            {
                InflateBalloon();
                if (balloon.transform.localScale.x >= balloonPopSize)
                {
                    hasPopped = true;
                }
            }
            else
            {
               WinConditionMet();
            }
        }
        

    }

    public void InflateBalloon()
    {
        Debug.Log("------------------------------Inflate Balloon-------------------------");
        balloon.transform.localScale += Vector3.one;
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }


    public override void WinConditionMet()
    {
        Debug.Log("WIN CONDITION MET");
        //last line must be...
        Win();
    }

    public override void FailConditionMet()
    {
        Debug.Log("FAIL CONDITION MET");
        //last line must be...
        Fail();
    }
}
