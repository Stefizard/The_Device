using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitchBehaviour : MonoBehaviour
{
    [SerializeField] DoorBehaviour doorBehaviour;
    [SerializeField] float range;
    [SerializeField] TextAnimation NeedKeyText;
    [SerializeField] TextAnimation OpenDoorText;

    bool isActivated = false;

    private void OnMouseOver()
    {
        Vector3 distance = Camera.main.transform.position - transform.position;
        if (!PauseMenu.paused && !isActivated && distance.sqrMagnitude <= range * range)
        {
            if (PlayerController.haveKey)
            {
                OpenDoorText.IncreaseOpacity();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenDoorText.ResetOpacity();
                    isActivated = true;
                    doorBehaviour.OpenDoors();
                }
            }
            else
            {
                NeedKeyText.IncreaseOpacity();
            }
        }
        else
        {
            NeedKeyText.ResetOpacity();
            OpenDoorText.ResetOpacity();
        }
    }

    private void OnMouseExit()
    {
        NeedKeyText.ResetOpacity();
        OpenDoorText.ResetOpacity();
    }
}
