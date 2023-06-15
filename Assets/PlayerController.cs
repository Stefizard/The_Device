using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] float groundDrag = 2.5f;

    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpCooldown = 0.1f;
    bool readyToJump = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1 )
        {
            Destroy(gameObject);
        }
    }

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadSceneAsync(1);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
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

    private void GetMovementKeys()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    private void MovePlayer()
    {
        if (horizontalInput != 0)
        {
            rigidBody.AddRelativeForce(Vector3.right * 3.8f * walkSpeed * horizontalInput);
        }
        if (verticalInput != 0)
        {
            rigidBody.AddRelativeForce(Vector3.forward * 3.8f * walkSpeed * verticalInput);
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
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
            rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke("ResetJump", jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
