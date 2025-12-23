using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float gravityMultiplier = 4f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;

    [SerializeField] private PlayerAnimatorController animatorController;
    
    
    private Vector3 moveInput;
    private bool jumpRequested;
    private bool isGrounded;
    private Vector3 lastFramePosition;

    private void Start()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
        
        if (animatorController == null)
            Debug.LogError($"{name} missing PlayerAnimatorController");
        
        lastFramePosition = transform.position;
    }

    private void Update()
    {
        // Reset and read movement input
        moveInput = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveInput += transform.forward;

        if (Input.GetKey(KeyCode.S))
            moveInput -= transform.forward;

        if (Input.GetKey(KeyCode.A))
            moveInput -= transform.right;

        if (Input.GetKey(KeyCode.D))
            moveInput += transform.right;

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
            jumpRequested = true;
        
        if (!jumpRequested)
            animatorController.SetMovementSpeed(Mathf.RoundToInt(Vector3.Distance(transform.position,lastFramePosition)/Time.fixedDeltaTime));
    }

    private void FixedUpdate()
    {
        UpdateGroundedStatus();

        // Movement
        Vector3 movement = moveInput.normalized * acceleration;
        rigidBody.AddForce(movement, ForceMode.Acceleration);

        // Jump
        if (jumpRequested && isGrounded)
        {
            animatorController.PlayJump();
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        jumpRequested = false;

        // Clamp horizontal speed
        ClampHorizontalSpeed();
        
        // Apply manual physics
        rigidBody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        
        lastFramePosition = transform.position; 
    }

    private void UpdateGroundedStatus()
    {
        // Raycast straight down from the object's position
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            transform.localScale.y + .2f,
            groundMask
        );
    }

    private void ClampHorizontalSpeed()
    {
        Vector3 vel = rigidBody.linearVelocity;

        // Horizontal velocity only (XZ)
        Vector3 horizontalVel = new Vector3(vel.x, 0f, vel.z);

        if (horizontalVel.magnitude > maxSpeed)
        {
            horizontalVel = horizontalVel.normalized * maxSpeed;
        }

        rigidBody.linearVelocity = new Vector3(horizontalVel.x, vel.y, horizontalVel.z);
    }
}
