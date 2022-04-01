using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRegistration : MonoBehaviour
{
    [SerializeField] public GameObject huntTheDuck;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Duck"))
        {
            huntTheDuck.GetComponent<HuntTheDuck>().hasDuck = true;
            Debug.Log("HitDuck");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        huntTheDuck.GetComponent<HuntTheDuck>().hasDuck = false;
    }
}
