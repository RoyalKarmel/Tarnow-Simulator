using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    [Header("Player settings")]
    public float speed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float crouchScale = 0.75f;
    float originalHeight;
    Vector3 normalScale;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;

    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        originalHeight = characterController.height;
        normalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Movement();
        ApplyGravity();

        // Crouching
        if (Input.GetKey(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    #region CheckGround
    // Check if player is on the ground
    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }
    #endregion

    #region Player movement
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    #endregion

    #region Player gravity
    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region Crouching
    void StartCrouch()
    {
        characterController.height = crouchScale;
        transform.localScale = new Vector3(transform.localScale.x, crouchScale / normalScale.y, transform.localScale.z);
        if (isGrounded)
            characterController.Move(Vector3.down * ((originalHeight - crouchScale) / 2));
    }

    void StopCrouch()
    {
        characterController.height = originalHeight;
        transform.localScale = normalScale;
        if (isGrounded)
            characterController.Move(Vector3.up * ((crouchScale - originalHeight) / 2));
    }
    #endregion
}
