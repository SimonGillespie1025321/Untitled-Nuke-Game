using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParachuteJump : MicroGame
{



    [SerializeField] public  float thrustyJump = 1.0f;
    [SerializeField] public float thrustxJump = 1.0f;
    [SerializeField] public float thrustyShoot = 1.0f;
    [SerializeField] public float thrustxShoot = 1.0f;
    [SerializeField] public float chuteDrag = 1.0f;
    [SerializeField] public float mass = 85f;
    [SerializeField] private GameObject skydiver;
    [SerializeField] private GameObject parachute;
    private Rigidbody rb;
    private bool HasJumped = false;
    private bool PullChute = false;

    private void OnEnable()
    {
    }

    public void Start()
    {
        Initialise();
        

    }


    public override void Initialise()
    {
        base.Initialise();     
    }





    //Game logic goes here
    public override void Tap()
    {
        if (isPlaying)
        {
            if (!HasJumped)
            {
                Jump();
            }
            else if (!PullChute)
            {
                Chute();
            }
        }

    }

    



    private void Jump()
    {
        // skydiver

        if (skydiver.TryGetComponent<Rigidbody>(out rb)) 
        {
            rb.mass = mass;
            rb.useGravity = true;
            rb.AddForce(thrustxJump, thrustyJump, 0, ForceMode.Impulse);
            HasJumped = true;
            transform.parent = null;
            Debug.Log("Skydiver Jumped");
        }
    }
    private void Chute()
    {
        //parachute
        if (parachute != null)
        {
            parachute.SetActive(true);
            rb.AddForce(thrustxShoot, thrustyShoot, 0, ForceMode.Impulse);
            rb.drag = chuteDrag;
            rb.mass = mass / 3;
            Debug.Log("Skydiver Pulled Shoot");
            PullChute = true;
        }
        else Debug.Log("THERES NO CHUTE!!!");
    }


    public override void WinConditionMet()
    {   

        //last line must be...
        Win();
    }

    public override void FailConditionMet()
    {

        //last line must be...
        Fail();
    }
}



