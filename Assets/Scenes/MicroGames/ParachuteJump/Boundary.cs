using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : ParachuteJumpBoundsCheck
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Destroy(other.transform.parent.gameObject);
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

}
