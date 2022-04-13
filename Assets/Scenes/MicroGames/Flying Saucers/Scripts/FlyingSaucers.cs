using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucers : MicroGame
{
    public bool hasEarth = false;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }
    public override void Tap()
    {
        if (isPlaying)
        {
            Debug.Log("Tap IsPlaying");
            if (hasEarth)
            {
                WinConditionMet();
            }
            else
            {
                FailConditionMet();
            }
        }
    }
    public override void FailConditionMet()
    {
        Debug.Log("FailConditionMet");
        Fail();
    }

    public override void WinConditionMet()
    {
        Debug.Log("WinConditionMet");
        Win();
    }
}
