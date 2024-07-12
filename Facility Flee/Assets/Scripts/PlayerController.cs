using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    public GameObject Acid;
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject pausedText;
    public GameObject playerPrefab;
    private CharacterController characterController;
    public float mouseSens;
    private float tarXRotation = 0;
    private float tarYRotation = 0;
    private Rigidbody plrRb;
    private AudioSource playerAudio;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip wallJump;
    public bool playingGame;

    public float speed;
    public float jumpForce;
    public float friction = 0.8f;

    private float camZRot = 0;

    public int dashLim;
    public int curDashes;
    public float dashDist;

    public int wallJumpLim;
    public int curWallJumps;

    public float wallJumpIdx = 0;
    private float wallJumpCamTar = 0;

    // Tempoary
    public GameObject end;

    public Slider sliderMouseSens;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        plrRb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.down * 17;
        playerAudio = GetComponent<AudioSource>();
        
    }

    // Tempoary
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == end)
        {
            SceneManager.LoadScene("Level");
            playerPrefab.GetComponent<PlayerController>().playingGame = true;
        }
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

        if (wallJumpIdx > 0 || wallJumpIdx < 0)
        {
            camZRot += (wallJumpCamTar - camZRot) / 25;
        } else
        {
            camZRot += (-Input.GetAxis("Horizontal") * 7 - camZRot) / 25;
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
            if (!isOnWall())
            {
                tempVel += transform.right * (speed / 20) * horizontalInput;
                tempVel += transform.forward * (speed / 20) * verticalInput;
                tempVel *= 0.99f;
            }
        }

        tempVel.y = tempYVel;
        plrRb.velocity = tempVel;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() || Input.GetKeyDown(KeyCode.Space) && isOnWall())
        {
            Vector3 setVelocity = plrRb.velocity;
            if (isOnWall() && !isGrounded())
            {
                if (curWallJumps > 0)
                {
                    if (Physics.Raycast(transform.position, transform.right, 0.6f))
                    {
                        playerAudio.PlayOneShot(wallJump, 0.6f);
                        setVelocity = -transform.right * 20;
                        wallJumpIdx = -1;
                        
                    }
                    if (Physics.Raycast(transform.position, -transform.right, 0.6f))
                    {
                        playerAudio.PlayOneShot(wallJump, 0.6f);
                        setVelocity = transform.right * 20;
                        wallJumpIdx = 1;
                       
                    }
                    if (Physics.Raycast(transform.position, transform.forward, 0.6f))
                    {
                        playerAudio.PlayOneShot(wallJump, 0.6f);
                        setVelocity = -transform.forward * 20;
                    }
                    if (Physics.Raycast(transform.position, -transform.forward, 0.6f))
                    {
                        playerAudio.PlayOneShot(wallJump, 0.6f);
                        setVelocity = transform.forward * 20;
                    }
                    transform.Translate(new Vector3(0, 1, 0));
                    setVelocity.y = jumpForce;
                    plrRb.velocity = setVelocity;
                    curWallJumps -= 1;
                    playerAudio.PlayOneShot(wallJump, 0.6f);
                }
                else
                {
                    if (Physics.Raycast(transform.position, transform.right, 0.6f))
                    {
                       
                        playerAudio.PlayOneShot(jump, 0.6f);
                        setVelocity = -transform.right * 20;
                    }
                    if (Physics.Raycast(transform.position, -transform.right, 0.6f))
                    {

                        playerAudio.PlayOneShot(jump, 0.6f);
                        setVelocity = transform.right * 20;
                    }
                    if (Physics.Raycast(transform.position, transform.forward, 0.6f))
                    {

                        playerAudio.PlayOneShot(jump, 0.6f);
                        setVelocity = -transform.forward * 20;
                    }
                    if (Physics.Raycast(transform.position, -transform.forward, 0.6f))
                    {

                        playerAudio.PlayOneShot(jump, 0.6f);
                        setVelocity = transform.forward * 20;
                    }
                    transform.Translate(new Vector3(0, 1, 0));
                    setVelocity.y = -jumpForce * 3;
                    plrRb.velocity = setVelocity;
                    playerAudio.PlayOneShot(jump, 0.6f);
                }


            }
            else
            {

                playerAudio.PlayOneShot(jump, 0.6f);
                setVelocity.y = jumpForce;
                plrRb.velocity = setVelocity;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            pausedText.SetActive(true);
            resumeButton.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && curDashes > 0 && !isGrounded())
        {
            curDashes--;
            cam.GetComponent<Camera>().fieldOfView = 120;
            plrRb.velocity = cam.transform.forward * dashDist;
            playerAudio.PlayOneShot(dash, 0.6f);
        }

        if (isGrounded())
        {
            curDashes = dashLim;
            curWallJumps = wallJumpLim;
        }

        if (wallJumpIdx > 0)
        {
            wallJumpIdx++;
            wallJumpCamTar = wallJumpIdx;
            if (wallJumpIdx > 30)
            {
                wallJumpIdx = 0;
                wallJumpCamTar = 0;
            }
        } else if (wallJumpIdx < 0)
        {
            wallJumpIdx--;
            wallJumpCamTar = wallJumpIdx;
            if (wallJumpIdx < -30)
            {
                wallJumpIdx = 0;
                wallJumpCamTar = 0;
            }
        }


        //Set cursor lock state depending on what key is pressed
        Cursor.lockState = CursorLockMode.None;

        
        
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitButtonPressed()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene("UI SCENE");
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.3f);
    }
    bool isOnWall()
    {
        return Physics.Raycast(transform.position, transform.right, 0.6f) ||
            Physics.Raycast(transform.position, -transform.right, 0.6f) ||
            Physics.Raycast(transform.position, -transform.forward, 0.6f) ||
            Physics.Raycast(transform.position, transform.forward, 0.6f);
    }
}
