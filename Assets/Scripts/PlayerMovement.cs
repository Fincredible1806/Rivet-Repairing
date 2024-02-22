using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement_Things")]
    private float moveSpeed = 50f;
    public float walkSpeed;
    public float runSpeed;

    public float groundDrag;

    [Header("Jumping_Things")]
    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump;
    public float airMultiplier;

    [Header("User_Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground_Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope handlers")]

    public float highestSlopeAngle;
    private RaycastHit slopeHitter;
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movingDirection;

    Rigidbody rigidB;

    public MovementState currentState;

    public enum MovementState
    {
        walking,
        sprinting,
        air,
    }

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

        // Player state handling
        StateHandler();

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

    private void StateHandler()
    {
        // Ensuring player state is handled

        // If statement for Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            currentState = MovementState.sprinting;
            moveSpeed = runSpeed;
        }

        // If statement for walking
        else if (grounded)
        {
            currentState = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Getting if player is in the air

        else 
        { 
            currentState = MovementState.air; 
        }

    }


    private void MovePlayer()
    {
        // Calculation of the movement direction
        movingDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On slope
        if(OnSlope() && !exitingSlope)
        {
            rigidB.AddForce(GetSlopeMoveDir() * moveSpeed * 2f, ForceMode.Force);

            if (rigidB.velocity.y > 0)
                rigidB.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // Ground Check
        if(grounded)
            rigidB.AddForce(movingDirection.normalized * moveSpeed, ForceMode.Force);

        // In air
        else if(!grounded)
            rigidB.AddForce(airMultiplier * moveSpeed * movingDirection.normalized, ForceMode.Force);

        // Turning off gravity when sloping
        rigidB.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // ensuring speed is handled on slopes
        if (OnSlope() && !exitingSlope) 
        {
            if(rigidB.velocity.magnitude > moveSpeed)
                rigidB.velocity = rigidB.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rigidB.velocity.x, 0f, rigidB.velocity.z);

            // limit Velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rigidB.velocity = new Vector3(limitedVel.x, rigidB.velocity.y, limitedVel.z);
            }
        }
       
    }

    private void Jump()
    {
        exitingSlope = true;

        // Reset Y Velocity
        rigidB.velocity = new Vector3(rigidB.velocity.x, 0f, rigidB.velocity.z);

        rigidB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;

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

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out slopeHitter, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHitter.normal);
            return angle < highestSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(movingDirection, slopeHitter.normal).normalized;
    }
}
