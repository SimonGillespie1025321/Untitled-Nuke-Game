using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloonpopboundary : ParachuteJumpBoundsCheck
{
    // Start is called before the first frame update

    [SerializeField] GameObject microGame;



    // Same as plane tag but changed for elephant


    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Elephantfoottag")
        {
            Debug.Log("Elephant went off screen");
            microGame.GetComponent<MicroGame>().FailConditionMet();
            Destroy(other.gameObject);
        }
    }


    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

}

