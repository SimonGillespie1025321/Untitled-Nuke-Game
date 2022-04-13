using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISeeRed : MicroGame
{
    public bool isRed = false;
    public GameObject iSeeRedPicturesObject;
    public ISeeRedPictures iSeeRedPictures;
    // Start is called before the first frame update
    void Start()
    {
        Initialise();
        if (iSeeRedPicturesObject.TryGetComponent<ISeeRedPictures>(out iSeeRedPictures))
        {
            iSeeRedPictures.Initialise();
            iSeeRedPictures.isPlaying = isPlaying;
        }
        else
            Debug.Log(this.name + ":  No ISeeRedPictures");
    }

    // Update is called once per frame
    

    public override void Tap()
    {
        if (isPlaying)
        {
            if (isRed)
                WinConditionMet();
            else
                FailConditionMet();
        }

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
