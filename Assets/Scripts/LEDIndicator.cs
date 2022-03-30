using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDIndicator : MonoBehaviour
{


    [SerializeField] public GameObject LEDPrefab;
    [SerializeField] public float LEDSpacing;
    public List<GameObject> indicators;
    private int numberOFLED;
    private int currentLED = 0;

    // Start is called before the first frame update


    private void Start()
    {
        Initialise();
    }

    private void OnEnable()
    {
        EventManager.increaseIndictor += LightNextLED;
    }

    private void OnDisable()
    {
        EventManager.increaseIndictor -= LightNextLED;
    }

    public void Initialise()
    {

        numberOFLED = GameManager.Instance.gameConfig.microgamesToWin;

        float ledPositionOffset = 0;
 
        for (int index = 0; index < numberOFLED; index++)
        {
            if (index != 0) { ledPositionOffset = ledPositionOffset + LEDSpacing; }
            
            Vector3 ledLocation = new Vector3(this.transform.position.x + ledPositionOffset, this.transform.position.y, this.transform.position.z);

            Instantiate(LEDPrefab, ledLocation, Quaternion.identity, this.transform);
        }

        //Debug.Log("child count: " + this.transform.childCount);
        for (int index = 0; index < this.transform.childCount; index++)
        {
            GameObject childObject = this.transform.GetChild(index).gameObject;
            if (childObject.CompareTag("LED"))
            { 
                childObject.name = "LED" + index;
                indicators.Add(childObject);
            }
            
        }

        
    }

    public void LightNextLED()
    {
        indicators[currentLED].GetComponent<LEDLight>().TurnLEDPositive();
        currentLED++;
        CheckHasWon();

    }

    private void CheckHasWon()
    {
       if (currentLED >= numberOFLED)
            EventManager.Instance.NukeHasBeenStopped();

    }

    public void ResetIndicator(Utility.LEDState ledState)
    {
       for (int index=0; index < indicators.Count; index++)
        {
            switch(ledState)
            {
                case Utility.LEDState.Off:
                    {
                        indicators[index].GetComponent<LEDLight>().TurnLEDOff();
                        break;
                    }
                case Utility.LEDState.Positive:
                    {
                        indicators[index].GetComponent<LEDLight>().TurnLEDPositive();
                        break;
                    }
                case Utility.LEDState.Negative:
                    {
                        indicators[index].GetComponent<LEDLight>().TurnLEDNegative();
                        break;
                    }
            }

        }
    }

}
