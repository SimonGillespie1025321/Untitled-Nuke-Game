using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroGameCollection : MonoBehaviour
{
    [SerializeField] public MicroGameDefinition[] microGameDefinition;
    public int numberOfMicrogames;

    public void Start()
    {
        if (microGameDefinition != null)
        {
            numberOfMicrogames = microGameDefinition.Length;
        }
    }


}
