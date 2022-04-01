using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    Ray ray;
    Vector3 forward;
    Vector3 crosshairPosition;
    [SerializeField] public float distanceToCrosshair;

    [SerializeField] public GameObject crosshair;

    // Update is called once per frame
    void Update()
    {
        forward = transform.TransformDirection(Vector3.right);
        ray = new Ray(transform.position, forward);
        crosshairPosition = ray.GetPoint(distanceToCrosshair);
        crosshair.transform.position = crosshairPosition;




        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 40f))
        // {
        //   Debug.Log("Hit Something");
        // }
        //else
        //{
        //  Debug.Log("Hit Nothing");
        //}
    }
}   







