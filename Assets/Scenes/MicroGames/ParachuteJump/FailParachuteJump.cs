using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailParachuteJump : MonoBehaviour
{

    [SerializeField] GameObject parachute;
    private void OnTriggerEnter(Collider other)
    {
        // parachute.SetActive(false);
        if (other.tag == "Player")
        {
        other.gameObject.SetActive(false);
        GetComponentInParent<MicroGame>().FailConditionMet();
        }
    }

    
}
