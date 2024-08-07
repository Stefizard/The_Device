using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    float camRotation = 0f;
    float camRotationThisFrame, xMouse, yMouse;
    float cameraVerticalAngleLimit = 90f;
    //TODO: mouse sensitivity, custom keys

    float horizontalInput;
    float verticalInput;

    Rigidbody rigidBody;
    [SerializeField] float walkSpeed = 6f;

    bool isGrounded;
    [SerializeField] float playerHeight = 2f;
    [SerializeField] float groundDrag = 2.5f;
    [SerializeField] float airMultiplier = 0.25f;

    [SerializeField] float jumpForce = 5f;
    bool jumping = false;
    float jumpTime;
    [SerializeField] float maxJumpTime = 0.25f;

    public bool canTeleport = true;
    [SerializeField] float sublevelDistance = 300f;
    int sublevel = 1;

    public static bool haveKey = false;

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
        GetMovementKeys();
        LimitSpeed();
        VerifyIfGrounded();
        ApplyDrag();
        Jump();
        Teleport();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetMouseInput()
    {
        xMouse = Input.GetAxis("Mouse X");
        yMouse = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * xMouse * System.Math.Min(0.02f,Time.deltaTime) * 200);
        LimitVerticalAngle();
        cam.transform.Rotate(Vector3.left * camRotationThisFrame);
        camRotation += camRotationThisFrame;
    }

    private void LimitVerticalAngle()
    {
        camRotationThisFrame = yMouse * System.Math.Min(0.02f, Time.deltaTime) * 200;
        if (camRotation + camRotationThisFrame > cameraVerticalAngleLimit)
        {
            camRotationThisFrame = cameraVerticalAngleLimit - camRotation;
        }
        else if (camRotation + camRotationThisFrame < -cameraVerticalAngleLimit)
        {
            camRotationThisFrame = -cameraVerticalAngleLimit - camRotation;
        }
    }

    private void GetMovementKeys()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        if (isGrounded)
        {
            if (horizontalInput != 0)
            {
                rigidBody.AddRelativeForce(Vector3.right * 10f * walkSpeed * horizontalInput);
            }
            if (verticalInput != 0)
            {
                rigidBody.AddRelativeForce(Vector3.forward * 10f * walkSpeed * verticalInput);
            }
        }
        else
        {
            if (horizontalInput != 0)
            {
                rigidBody.AddRelativeForce(Vector3.right * 10f * walkSpeed * horizontalInput * airMultiplier);
            }
            if (verticalInput != 0)
            {
                rigidBody.AddRelativeForce(Vector3.forward * 10f * walkSpeed * verticalInput * airMultiplier);
            }
        }
    }

    private void LimitSpeed()
    {
        Vector3 horizontalVelocity = new Vector3 (rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if (horizontalVelocity.magnitude > walkSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * walkSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }

    private void VerifyIfGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight / 2f + 0.1f) && hit.transform.gameObject.CompareTag("Ground"))
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
            rigidBody.drag = 0;
        }
    }

    private void Jump()
    {
        if (!PauseMenu.paused && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
            jumpTime = 0;
        }
        if (jumping)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
            jumpTime += Time.deltaTime;
        }
        if (jumpTime > maxJumpTime | Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }
    }
    private void Teleport()
    {
        if (!PauseMenu.paused && canTeleport && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (sublevel == 1)
            {
                transform.position = transform.position + transform.up * sublevelDistance;
                sublevel = 2;
            }
            else if (sublevel == 2)
            {
                transform.position = transform.position + transform.up * -sublevelDistance;
                sublevel = 1;
            }
        }
    }
}
