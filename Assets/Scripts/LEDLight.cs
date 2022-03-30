using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDLight : MonoBehaviour
{

    public Material colorLEDOff;
    public Material colorLEDNegative;
    public Material colorLEDPositive;

    public GameObject innerCase;
    public GameObject outerCase;

    private Renderer innerCaseRenderer;
    private Renderer outerCaseRenderer;

    // Start is called before the first frame update
    void OnEnable()
    {
        innerCaseRenderer = innerCase.GetComponent<Renderer>();
        outerCaseRenderer = outerCase.GetComponent<Renderer>();
        TurnLEDOff();
        
    }

    public void TurnLEDOff()
    {
        innerCaseRenderer.material = colorLEDOff;
        outerCaseRenderer.material = colorLEDOff;
    }
    public void TurnLEDPositive()
    {
        Debug.Log("TurnLEDPositive called");
        innerCaseRenderer.material = colorLEDPositive;
        outerCaseRenderer.material = colorLEDPositive;

    }
    public void TurnLEDNegative()
    {
        innerCaseRenderer.material = colorLEDNegative;
        outerCaseRenderer.material = colorLEDNegative;

    }

}
