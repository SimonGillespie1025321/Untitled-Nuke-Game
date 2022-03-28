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
            Debug.Log("Fail Jump");
            other.gameObject.SetActive(false);

           
                GetComponentInParent<MicroGame>().FailConditionMet();
            
        }
    }

    
}
