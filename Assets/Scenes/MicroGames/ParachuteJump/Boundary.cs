using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : ParachuteJumpBoundsCheck
{
    // Start is called before the first frame update

    [SerializeField] GameObject microGame;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
             microGame.GetComponent<MicroGame>().FailConditionMet();
             Destroy(other.gameObject);
        }
        if (other.tag == "Plane")
        {
            Destroy(other.gameObject);
        }
    }
    

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

}
