using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private BARSmanagerScript bARSmanagerScript;
    private AudioManager audioManager;
    public Camera playerCamera;
    public float speed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 5.0f;
    public float gravity = 9.81f;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public int ScrapMetal = 0;
    public int BatteriesCreated = 0;

    Vector3 moveDirection = Vector3.zero;       

    float rotationX = 0;

    public bool canMove = true; 
    public float bounceForce = 12f;

    private AudioSource FootSteps;

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        bARSmanagerScript = FindAnyObjectByType<BARSmanagerScript>();
        audioManager = FindAnyObjectByType<AudioManager>();
        FootSteps = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (BatteriesCreated == 5)
        {
            SceneManager.LoadScene(3);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : speed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : speed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


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

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Handle Footstep Audio
        bool isMoving = (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f) 
                        && characterController.isGrounded;

        if (isMoving)
        {
            if (!FootSteps.isPlaying)
                FootSteps.Play();
        }
        else
        {
            if (FootSteps.isPlaying)
                FootSteps.Stop();
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
