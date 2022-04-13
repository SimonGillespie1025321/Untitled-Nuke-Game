using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRegistrationFS : MonoBehaviour
{
    [SerializeField] public GameObject flyingSaucers;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Earth"))
        {
            flyingSaucers.GetComponent<FlyingSaucers>().hasEarth = true;
            Debug.Log("HitEarth");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        flyingSaucers.GetComponent<FlyingSaucers>().hasEarth = false;
    }
}