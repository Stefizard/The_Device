using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] TextAnimation GetKeyText;

    private void OnMouseOver()
    {
        Vector3 distance = Camera.main.transform.position - transform.position;
        if (!PauseMenu.paused && distance.sqrMagnitude <= range * range)
        {
            GetKeyText.IncreaseOpacity();
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetKeyText.ResetOpacity();
                PlayerController.haveKey = true;
                gameObject.SetActive(false);
            }
        }
        else
        {
            GetKeyText.ResetOpacity();
        }
    }

    private void OnMouseExit()
    {
        GetKeyText.ResetOpacity();
    }
}
