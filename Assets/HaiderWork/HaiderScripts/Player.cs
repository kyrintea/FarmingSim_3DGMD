using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("References")]
    private BARSmanagerScript bARSmanagerScript;
    private AudioManager audioManager;
    public Camera playerCamera;
    private CharacterController characterController;
    private AudioSource footSteps;

    [Header("Movement")]
    public float speed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 5.0f;
    public float gravity = 9.81f;
    public float smoothTime = 0.1f;

    [Header("Look")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Game State")]
    public int ScrapMetal = 0;
    public int BatteriesCreated = 0;
    public float bounceForce = 12f;
    public bool canMove = true;

    // Private variables
    private Vector3 moveDirection;
    private Vector3 moveVelocity;
    private Vector3 smoothVelocityRef;
    private float rotationX;
    private float yaw;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footSteps = GetComponent<AudioSource>();
        bARSmanagerScript = FindAnyObjectByType<BARSmanagerScript>();
        audioManager = FindAnyObjectByType<AudioManager>();

        yaw = transform.eulerAngles.y;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CheckWinCondition();
        HandleMouseLook();
        HandleMovement();
        HandleFootsteps();
    }

    void FixedUpdate()
    {
        characterController.Move(moveDirection * Time.fixedDeltaTime);
    }

    void CheckWinCondition()
    {
        if (BatteriesCreated >= 5)
        {
            SceneManager.LoadScene(3);
        }
    }

    void HandleMouseLook()
    {
        if (!canMove) return;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        rotationX -= mouseY * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        yaw += mouseX * lookSpeed;

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }

    void HandleMovement()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : speed;
        
        float verticalInput = canMove ? Input.GetAxis("Vertical") : 0;
        float horizontalInput = canMove ? Input.GetAxis("Horizontal") : 0;

        Vector3 targetVelocity = (forward * verticalInput + right * horizontalInput) * currentSpeed;

        // Smooth horizontal movement
        Vector3 smoothedHorizontal = Vector3.SmoothDamp(
            new Vector3(moveVelocity.x, 0, moveVelocity.z),
            new Vector3(targetVelocity.x, 0, targetVelocity.z),
            ref smoothVelocityRef,
            smoothTime
        );

        moveDirection.x = smoothedHorizontal.x;
        moveDirection.z = smoothedHorizontal.z;

        // Handle jumping and gravity
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else if (moveDirection.y < 0)
        {
            moveDirection.y = -2f;
        }

        moveVelocity = moveDirection;
    }

    void HandleFootsteps()
    {
        bool isMoving = (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) && characterController.isGrounded;

        if (isMoving && !footSteps.isPlaying)
        {
            footSteps.Play();
        }
        else if (!isMoving && footSteps.isPlaying)
        {
            footSteps.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            bARSmanagerScript.TakeDamage(10f);
        }
        else if (other.CompareTag("Oxygen"))
        {
            bARSmanagerScript.HealOxygen(100f);
            moveDirection.y = bounceForce;
            audioManager.boost();
        }
    }
}