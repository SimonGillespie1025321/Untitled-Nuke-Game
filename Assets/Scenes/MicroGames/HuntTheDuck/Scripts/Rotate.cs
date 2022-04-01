using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 60f;
    [SerializeField] float startAngle = 90f;
    [SerializeField] float endAngle = 45f;



    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * rotateSpeed, startAngle) - endAngle);
    }
}
