using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Main Game Package")]

public class MainGameConfiguration : ScriptableObject
{
    public int microgamesToWin;
    public int timerInSeconds;

}
