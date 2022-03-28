using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParachuteJump : MonoBehaviour
{
    [SerializeField] GameObject parachute;
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parachute.SetActive(false);
            GetComponentInParent<MicroGame>().WinConditionMet();
        }
    }

    

}
