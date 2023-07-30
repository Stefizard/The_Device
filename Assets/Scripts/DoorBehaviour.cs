using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] TranslateAnimation leftDoor;
    [SerializeField] TranslateAnimation rightDoor;
    public void OpenDoors()
    {
        leftDoor.Play();
        rightDoor.Play();
    }
}
