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
    public float dashDistance = 50;

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

        if (IsGrounded())
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Vector3 setVelocity = plrRb.velocity;
            setVelocity.y = jumpForce;
            plrRb.velocity = setVelocity;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && curDashes > 0 && !IsGrounded())
        {
            curDashes--;

            cam.GetComponent<Camera>().fieldOfView = 120;
            plrRb.velocity = cam.transform.forward * dashDistance;
        }

        if (IsGrounded())
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

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.3f);
    }
}
