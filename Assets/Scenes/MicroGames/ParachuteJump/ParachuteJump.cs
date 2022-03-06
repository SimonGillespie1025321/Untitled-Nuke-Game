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
    [SerializeField] private GameObject target;
    private Rigidbody rb;
    public bool HasJumped = false;
    public bool PullChute = false;
    private float originalX;
    public Vector3 originalScale;

    private void OnEnable()
    {
    }

    public void Start()
    {
        Initialise();
        

    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public override void Initialise()
    {
        base.Initialise();
        originalX = target.transform.position.x;
        originalScale = target.transform.localScale;
        setTarget();
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


    public void setTarget()
    {
        Debug.Log(originalX.ToString());
        float newX = Random.Range(0, 25);
        float newScaleX = Random.Range(0.6f, 1f);
        Vector3 newScale = new Vector3(newScaleX, originalScale.y, originalScale.z);


        newX = newX + originalX;
        Debug.Log(newX.ToString());
        Vector3 newTargetPosition = new Vector3(newX, target.transform.position.y, target.transform.position.z);
        target.transform.position = newTargetPosition;
        target.transform.localScale = newScale;
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
        Debug.Log("WIN CONDITION MET");
        //last line must be...
        Win();
    }

    public override void FailConditionMet()
    {
        Debug.Log("FAIL CONDITION MET");
        //last line must be...
        Fail();
    }
}



