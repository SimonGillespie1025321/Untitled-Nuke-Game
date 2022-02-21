using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParachuteJump : MicroGame
{
 


    public  float thrustyJump = 1.0f;
    public float thrustxJump = 1.0f;
    public float thrustyShoot = 1.0f;
    public float thrustxShoot = 1.0f;
    public float chuteDrag = 1.0f;
    public float mass = 85f;
    private Rigidbody rb;
    [SerializeField] private GameObject parachute;
    private GameObject skydiver;
    private bool HasJumped = false;
    private bool PullShoot = false;



    private void OnEnable()
    {
        //IMPORTANT:  It is critical that sceneName be set to the scene name exactly
        sceneName = "ParachuteJump";
        //---------------
        PlayerController.nukeKeyPressed += TapPressed;
    }

    public void Start()
    {
        Initialise();
        

    }

    public override void Initialise()
    {
        base.Initialise();
        
        microGameType = Utility.MicroGameType.Tap;
        GameManager.Instance.loadedGameType = microGameType;
        PlayerController.Instance.SetKeyFunction(microGameType);
    }


    public void OnDestroy()
    {
        PlayerController.nukeKeyPressed -= TapPressed;
    }

    public void TapPressed()
    {

        if (!HasJumped)
        {
            Jump();
        }
        if (!PullShoot)
        { 
            Chute();
        }

    }
    


    private void Jump()
    {
        // skydiver

        if (TryGetComponent<Rigidbody>(out rb)) ;
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
        if (TryGetComponent<Rigidbody>(out rb));
        {
            parachute.SetActive(true);
            rb.AddForce(thrustxShoot, thrustyShoot, 0, ForceMode.Impulse);
            rb.drag = chuteDrag;
            rb.mass = mass / 2;
            Debug.Log("Skydiver Pulled Shoot");
            PullShoot = true;
        }
    }
}



