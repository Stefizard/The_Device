using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    float camRotation = 0f;
    float camRotationThisFrame, xMouse, yMouse;
    float cameraVerticalAngleLimit = 90f;

    Rigidbody rigidBody;
    [SerializeField] float walkSpeed = 500f;
    [SerializeField] float sideWalkSpeed = 350f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();
        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.forward * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddRelativeForce(Vector3.back * walkSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.left * sideWalkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right * sideWalkSpeed * Time.deltaTime);
        }
    }

    private void GetMouseInput()
    {
        xMouse = Input.GetAxis("Mouse X");
        yMouse = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * xMouse * Time.deltaTime * 200);
        LimitVerticalAngle();
        cam.transform.Rotate(Vector3.left * camRotationThisFrame);
        camRotation += camRotationThisFrame;
    }

    private void LimitVerticalAngle()
    {
        camRotationThisFrame = yMouse * Time.deltaTime * 200;
        if (camRotation + camRotationThisFrame > cameraVerticalAngleLimit)
        {
            camRotationThisFrame = cameraVerticalAngleLimit - camRotation;
        }
        else if (camRotation + camRotationThisFrame < -cameraVerticalAngleLimit)
        {
            camRotationThisFrame = -cameraVerticalAngleLimit - camRotation;
        }
    }
}
