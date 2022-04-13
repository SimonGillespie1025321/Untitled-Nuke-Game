using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySaucer : MonoBehaviour
{
    [SerializeField] public GameObject flyingSaucers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoundsCheck"))
        {
            Destroy(other.transform.parent.gameObject);
            flyingSaucers.GetComponent<HuntTheDuck>().FailConditionMet();
            Debug.Log("HitEarth");
            Debug.Log("bounds check");
        }
    }

}
