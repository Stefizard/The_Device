using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationMainMenu : MonoBehaviour
{   
    [SerializeField] float rotationSpeed = 0.7f;
    float mouseDistance;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    void Update()
    {
        mouseDistance = 2 * Input.mousePosition.x / Screen.width - 1;
        transform.Rotate(Vector3.down * mouseDistance * rotationSpeed * 100f * Time.deltaTime);
    }
}
