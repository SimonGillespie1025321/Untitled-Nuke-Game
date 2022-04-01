using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuck : MonoBehaviour
{
    [SerializeField] public GameObject huntTheDuck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundsCheck"))
        {
            Destroy(other.transform.parent.gameObject);
            huntTheDuck.GetComponent<HuntTheDuck>().FailConditionMet();
            Debug.Log("HitDuck");
            Debug.Log("bounds check");
        }
    }
    
}



