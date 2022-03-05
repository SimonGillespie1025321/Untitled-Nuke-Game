using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Main Game Configuration Settings")]
public class MainGameConfiguration : MonoBehaviour
{
    [SerializeField] public int microgamesToWin;
    [SerializeField] public int timerInSeconds;

}
