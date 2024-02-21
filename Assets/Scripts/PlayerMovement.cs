using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement_Things")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump;
    public float airMultiplier;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movingDirection;

    Rigidbody rigidB;

    private void Start()
    {
       readyToJump = true;
       rigidB = GetComponent<Rigidbody>();
       rigidB.freezeRotation = true;
    }

    private void Update()
    {
        //Checking grounded 
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Getting player input and limiting speed increase
        PlayerInput();
        SpeedControl();

        //Handle Drag
        DragHandling();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        // Taking in Horizontal and Vertical Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When the player jumps
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
     
    }


    private void MovePlayer()
    {
        // Calculation of the movement direction
        movingDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Ground Check
        if(grounded)
            rigidB.AddForce(movingDirection.normalized * moveSpeed, ForceMode.Force);

        // In air
        else if(!grounded)
            rigidB.AddForce(movingDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidB.velocity.x, 0f, rigidB.velocity.z);

        // limit Velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidB.velocity = new Vector3(limitedVel.x, rigidB.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset Y Velocity
        rigidB.velocity = new Vector3(rigidB.velocity.x, 0f, rigidB.velocity.z);

        rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

    }    

    void DragHandling()
    {
        if (grounded)
        {
            rigidB.drag = groundDrag;
        }
        else
        {
            rigidB.drag = 0;
        }
    }
}
