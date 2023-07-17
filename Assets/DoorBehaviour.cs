using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] float duration = 2f;
    [SerializeField] float doorWidth;
    public void OpenDoors()
    {
        
        leftDoor.GetComponent<TranslateAnimation>().Play(leftDoor.transform.position - new Vector3(doorWidth, 0, 0), duration);
        rightDoor.GetComponent<TranslateAnimation>().Play(rightDoor.transform.position + new Vector3(doorWidth, 0, 0), duration);
    }
}
