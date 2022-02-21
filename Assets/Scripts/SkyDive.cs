using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyDive : MonoBehaviour

{
    public float thrustyJump = 1.0f;
    public float thrustxJump = 1.0f;
    public float thrustyShoot = 1.0f;
    public float thrustxShoot = 1.0f;
    public float ShootDrag = 1.0f;
    public float Mass = 85f;
    public Rigidbody rb;
    public bool HasJumped = false;
    public bool PullShoot = false; 
    
    void Update()
    {
        //When the spacebar is pressed down
        if (Input.GetKeyDown(KeyCode.Space) && HasJumped == false)
        {
            gameObject.AddComponent<Rigidbody>();
            rb = GetComponent<Rigidbody>();
            rb.mass = Mass;
            rb.AddForce(thrustxJump, thrustyJump, 0, ForceMode.Impulse);
            Debug.Log("Skydiver Jumped");
            HasJumped = true;
            transform.parent =  null;
            

        }

        if (Input.GetKeyDown(KeyCode.P) && PullShoot == false && HasJumped == true) 
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(thrustxShoot, thrustyShoot, 0, ForceMode.Impulse);
            rb.drag = ShootDrag;
            Debug.Log("Skydiver Pulled Shoot");
            PullShoot = true;
            
        }

        //if skydiver has jumped && spacebar pressed 

        //Has the Skydiver landed force applied
    }
    
}
