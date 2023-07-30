using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchBehaviour : MonoBehaviour
{
    [SerializeField] DoorBehaviour doorBehaviour;
    [SerializeField] float range;

    bool isActivated = false;

    private void OnMouseOver()
    {
        Vector3 distance = Camera.main.transform.position - transform.position;
        if (!isActivated && distance.sqrMagnitude <= range * range && Input.GetKeyDown(KeyCode.E))
        {
                isActivated = true;
                doorBehaviour.OpenDoors();
        }
    }
}
