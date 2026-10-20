using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;

    public float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;

    public float jumpForce = 5.0f;
    public float gravity = -9.8f;

    private Rigidbody playerRB;

    public float strafeForce = 50.0f;
    public float strafeDecay = 5.0f;

    private Vector3 originalScale;
    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // movement vector (transform.right + transform.forward get unit vector relative to where player is facing)
        Vector3 move = (transform.right * horizontalInput + transform.forward * forwardInput);

        // movement from WASD input + external (strafing etc...)
        Vector3 totalMovement = (move * speed) + playerVelocity;


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        playerVelocity.y += (gravity * Time.deltaTime);


        //Strafe
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
