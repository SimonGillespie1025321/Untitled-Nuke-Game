using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{

    [SerializeField] GameObject parachute;
    private void OnTriggerEnter(Collider other)
    {
        // parachute.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
