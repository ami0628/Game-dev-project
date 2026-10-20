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

    public bool isOnGround = true;
    public float jumpForce = 5.0f;
    private Rigidbody playerRB;

    public float strafeForce = 30.0f;

    private Vector3 originalScale;

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



        //// move the player without going through walls!!!
        Vector3 move = new Vector3(horizontalInput, 0, forwardInput);
        controller.Move(move * speed * Time.deltaTime);




        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }


        //Strafe
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                playerRB.AddForce(Vector3.right * strafeForce * -1, ForceMode.Impulse);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                playerRB.AddForce(Vector3.right * strafeForce * 1, ForceMode.Impulse);
            }
        }

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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
