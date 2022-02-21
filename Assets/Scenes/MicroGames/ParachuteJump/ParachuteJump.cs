using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParachuteJump : MicroGameBase
{
    private bool jump = false;
    private bool pull = false;
    private bool landed = false;
    private bool inAir = false;
    
    private float groundLevel = -19f;
    private float pullAltitude = 5;
    
    private GameObject parachute;
    private GameObject skydiver;
    
    private Rigidbody skydiverRB;
    private Rigidbody parachuteRB;

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

        parachute = GameObject.Find("Parachute");

        skydiver = GameObject.Find("Skydiver Vertical");

        if (!skydiver.TryGetComponent<Rigidbody>(out skydiverRB))
            return;
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
        Debug.Log("TAP HAS BEEN PRESSED");

        if (!jump)
        {
            jump = true;
        }
        else if (inAir)
        { 
            pull = true; 
        }

    }
     
    public void FixedUpdate()
    {
        if (!landed)
        {
            if (jump & !inAir)
            {

                Debug.Log("jump");
                inAir = true;
                skydiverRB.useGravity = true;
                skydiverRB.AddForce(-11f, 3f, 0, ForceMode.Impulse);

            }
            else if (!pull && inAir)
            {
                Debug.Log("pull");
                skydiverRB.AddForce(9f, 1f, 0, ForceMode.Impulse);

            }
            else if (inAir && pull)
            {
                Debug.Log("float");
                Vector3 skydiverPos = skydiver.transform.position;
                skydiverPos.y = skydiverPos.y + 5;
                parachute.transform.position = skydiverPos;
                if (skydiver.transform.position.y <= groundLevel)
                {
                    landed = true;
                    skydiverRB.velocity = Vector3.zero;
                }
            }
            if (landed)
            {
                parachute.SetActive(false);
            }
        }

    }


}
