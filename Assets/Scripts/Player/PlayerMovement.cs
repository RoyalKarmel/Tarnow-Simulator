using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public PlayerStats stats;
    GameObject player;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Crouching variables
    float standingHeight;
    float crouchingHeight;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        player = PlayerManager.instance.player;

        standingHeight = player.transform.localScale.y;
        crouchingHeight = standingHeight * 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Player move
        Movement();

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        // Apply crouching if pressing crouch button
        if (Input.GetButtonDown("Crouch"))
            Crouch();
        if (Input.GetButtonUp("Crouch"))
            StandUp();

        // Apply gravity
        HandleGravity();

        // Sprint
        if (Input.GetButton("Sprint") && stats.currentStamina > 0 && !stats.isOverWeight)
            Sprint();
        else
            StopSprinting();
    }

    // Player movement
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * stats.speed * Time.deltaTime);
    }

    #region Jumping
    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region Crouching
    void Crouch()
    {
        player.transform.localScale = new Vector3(
            player.transform.localScale.x,
            crouchingHeight,
            player.transform.localScale.z
        );
    }

    void StandUp()
    {
        player.transform.localScale = new Vector3(
            player.transform.localScale.x,
            standingHeight,
            player.transform.localScale.z
        );
    }
    #endregion

    #region Sprint
    void Sprint()
    {
        stats.currentStamina -= stats.staminaCost * Time.deltaTime;
        if (stats.currentStamina < 0)
        {
            stats.currentStamina = 0;
            StopSprinting();
            return;
        }

        stats.speed = stats.sprintSpeed;
        stats.staminaBar.SetValue((int)stats.currentStamina);
    }

    void StopSprinting()
    {
        stats.speed = stats.moveSpeed;
    }
    #endregion
}
