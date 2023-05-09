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
    //TODO: mouse sensitivity, custom keys

    Rigidbody rigidBody;
    [SerializeField] float walkSpeed = 500f;

    bool isGrounded;
    [SerializeField] float groundDrag = 10f;

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
        RespondToMovementKeys();
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f) && hit.transform.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            rigidBody.drag = groundDrag;
        }
        else
        {
            rigidBody.drag = 0f;
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

    private void RespondToMovementKeys()
    {
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
            rigidBody.AddRelativeForce(Vector3.left * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right * walkSpeed * Time.deltaTime);
        }
    }
}
