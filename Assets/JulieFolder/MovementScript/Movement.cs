using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    private BARSmanagerScript bARSmanagerScript;
    public Camera playerCamera;
    public float speed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 5.0f;
    public float gravity = 9.81f;
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;
    public float footstepInterval = 0.5f;
    private float footstepTimer = 0f;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;       

    float rotationX = 0;

    public bool canMove = true; 

    CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        bARSmanagerScript = FindAnyObjectByType<BARSmanagerScript>();
    }

    void FixedUpdate()
    {
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

        HandleFootsteps();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heal"))
        {
            bARSmanagerScript.Heal(10f);
        }
        else if (other.CompareTag("Enemy"))
        {
            bARSmanagerScript.TakeDamage(10f);
        }
    }

    void HandleFootsteps()
    {
        if (!characterController.isGrounded) return;

        bool isMoving = (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f ||
                         Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f);

        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                PlayFootstep();
                float interval = Input.GetKey(KeyCode.LeftShift) ? footstepInterval * 0.6f : footstepInterval;
                footstepTimer = interval;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip);
    }
}
