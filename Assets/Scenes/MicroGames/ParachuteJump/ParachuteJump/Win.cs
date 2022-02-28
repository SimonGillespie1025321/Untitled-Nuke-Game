using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject parachute;
     
    private void OnTriggerEnter(Collider other)
    {
        parachute.SetActive(false);
    }
}
