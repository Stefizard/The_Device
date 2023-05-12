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
    [SerializeField] float walkSpeed = 6f;

    bool isGrounded;
    [SerializeField] float groundDrag = 2.5f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpCooldown = 0.1f;
    bool readyToJump = true;

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
        LimitSpeed();
        VerifyIfGrounded();
        ApplyDrag();
        Jump();
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
            rigidBody.AddRelativeForce(Vector3.forward * walkSpeed * 200 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddRelativeForce(Vector3.back * walkSpeed * 200 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.left * walkSpeed * 200 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right * walkSpeed * 200 * Time.deltaTime);
        }
    }

    private void LimitSpeed()
    {
        if (rigidBody.velocity.magnitude > walkSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * walkSpeed;
        }
    }

    private void VerifyIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f) && hit.transform.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void ApplyDrag()
    {
        if (isGrounded)
        {
            rigidBody.drag = groundDrag;
        }
        else
        {
            rigidBody.drag = 0f;
        }
    }

    private void Jump()
    {
        if (isGrounded && readyToJump && Input.GetKey(KeyCode.Space))
        {
            readyToJump = false;
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Invoke("ResetJump", jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
