using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MicroGame
{
    public bool hasPopped = false;
    
   


    public override void Mash()
    {
        if (isPlaying)
        {
            if (!hasPopped)
            {
                InflateBalloon();
            }
        }

    }

    public void InflateBalloon()
    {
        Debug.Log("Inflate Balloon");
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
