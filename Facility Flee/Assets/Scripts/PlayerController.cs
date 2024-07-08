/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float dashSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Rigidbody playerRb;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerRb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = walkSpeed * Input.GetAxis("Vertical");
        float curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX * 10) + (right * curSpeedY * 10);



        if (Input.GetKey(KeyCode.E) && characterController.isGrounded && canMove)
        {
            playerRb.AddForce(playerCamera.transform.forward * dashSpeed * Time.deltaTime, ForceMode.Impulse);
        }


        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion
        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    private CharacterController characterController;
    public float mouseSens;
    private float tarXRotation = 0;
    private float tarYRotation = 0;
    private Rigidbody plrRb;

    public float speed;
    public float jumpForce;
    public float friction = 0.8f;

    private float camZRot = 0;
    private float camZRotSpd = 0.02f;
    private float camZRotRange = 1;
    private float camZRotSpeedFull = 0.02f;

    private int dashLim = 1;
    private int curDashes = 1;
    private float dashDist = 50;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        plrRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the axis of both the X and Y mouse positions
        float horizontalMouseAxis = Input.GetAxis("Mouse X");
        float verticalMouseAxis = Input.GetAxis("Mouse Y");

        //Get the axis of the horizontal and vertical inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Make the rotation values add/subtract to the mouse X/Y
        tarXRotation -= verticalMouseAxis * (mouseSens);
        tarXRotation = Mathf.Clamp(tarXRotation, -90.0f, 90.0f);

        tarYRotation += horizontalMouseAxis * (mouseSens);

        //Apply the rotations
        transform.rotation = Quaternion.Euler(transform.rotation.x, tarYRotation, transform.rotation.z);
        cam.transform.rotation = Quaternion.Euler(tarXRotation, tarYRotation, camZRot);

        if (Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) > 0)
        {
            camZRot += camZRotSpd;
            if (camZRot > camZRotRange)
            {
                camZRotSpd = -camZRotSpeedFull;
            }
            if (camZRot < -camZRotRange)
            {
                camZRotSpd = camZRotSpeedFull;
            }
        }
        else
        {
            camZRot += (0 - camZRot) / 5;
        }

        cam.GetComponent<Camera>().fieldOfView += (100 - cam.GetComponent<Camera>().fieldOfView) / 25;



        //Add velocity to the player depending on the movement key presses
        Vector3 tempVel = plrRb.velocity;
        float tempYVel = tempVel.y;

        if (isGrounded())
        {
            tempVel += transform.right * speed * horizontalInput;
            tempVel += transform.forward * speed * verticalInput;
            tempVel *= friction;
        }
        else
        {
            tempVel += transform.right * (speed / 20) * horizontalInput;
            tempVel += transform.forward * (speed / 20) * verticalInput;
            tempVel *= 0.99f;
        }

        tempVel.y = tempYVel;
        plrRb.velocity = tempVel;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Vector3 setVelocity = plrRb.velocity;
            setVelocity.y = jumpForce;
            plrRb.velocity = setVelocity;
        }
        print(isGrounded());

        if (Input.GetKeyDown(KeyCode.LeftShift) && curDashes > 0 && !isGrounded())
        {
            curDashes--;
            Debug.Log(curDashes);

            cam.GetComponent<Camera>().fieldOfView = 120;
            plrRb.velocity = cam.transform.forward * dashDist;
        }

        if (isGrounded())
        {
            curDashes = dashLim;
        }


        //Set cursor lock state depending on what key is pressed
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.3f);
    }
}
