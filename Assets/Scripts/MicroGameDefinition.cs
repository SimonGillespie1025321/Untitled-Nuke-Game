using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "Micro Game Package")]

public class MicroGameDefinition : ScriptableObject
{
    [SerializeField] public string sceneName;
    [SerializeField] public string gameName;
    [SerializeField] public string description;
    [SerializeField] public string instructions;
    [SerializeField] public Utility.MicroGameType gameType;
    public  int sceneIndex = -1;
  
}
