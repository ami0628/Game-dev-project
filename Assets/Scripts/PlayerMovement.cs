using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public Transform playerCamera;

    public float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;

    public float jumpForce = 6.0f;
    public float gravity = -40f;
    private float groundedGravity = -2f;


    public float strafeForce = 100f;
    public float strafeDecay = 5.0f;
    private float strafeTimer = 0f;

    private Vector3 originalScale;
    private Vector3 playerVelocity;

    private float mouseXInput;
    private float mouseYInput;
    private float cameraPitch = 0.0f;  
    public float mouseSensitivity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        mouseXInput = Input.GetAxis("Mouse X");
        mouseYInput = Input.GetAxis("Mouse Y");

        // movement vector (transform.right + transform.forward get unit vector relative to where player is facing)
        Vector3 move = (transform.right * horizontalInput + transform.forward * forwardInput);

        // movement from WASD input + external (strafing etc...)
        Vector3 totalMovement = (move * speed) + playerVelocity;

        // mouse movement turns player / camera
        transform.Rotate(Vector3.up * mouseXInput * mouseSensitivity);
        cameraPitch -= (mouseYInput * mouseSensitivity);
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);


        //Jump
        if (controller.isGrounded && playerVelocity.y < 0) { playerVelocity.y = groundedGravity; }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        playerVelocity.y += (gravity * Time.deltaTime);


        //Strafe
        if (strafeTimer > 0f) { strafeTimer -= Time.deltaTime; }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    playerVelocity += (transform.right * strafeForce * -1);
                }

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    playerVelocity += (transform.right * strafeForce * 1);
                }

                if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
                {
                    playerVelocity += (transform.forward * strafeForce * 1);
                }
                strafeTimer = 1f;
            }
        }

        // decay strafe momentume
        //lerp (linear interpolation) gets value between current velocity and 0, amplified by the time/ strafe decay
        playerVelocity.x = Mathf.Lerp(playerVelocity.x, 0, strafeDecay * Time.deltaTime);
        playerVelocity.z = Mathf.Lerp(playerVelocity.z, 0, strafeDecay * Time.deltaTime);

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(originalScale.x, (originalScale.y/2), originalScale.z);
            transform.position -= transform.up * originalScale.y * 0.25f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            transform.position += transform.up * originalScale.y * 0.25f;
        }


        controller.Move(totalMovement * Time.deltaTime);

    }

}
