using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity=3.5f;
    [SerializeField] float jumpPower = 3.5f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float lookSmoothTime = 0.03f;



    public AudioClip move;
    public AudioSource audioSource;
    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector2 currenMouseDelta = Vector2.zero;
    Vector2 currenMouseDeltaVelocity = Vector2.zero;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }
    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currenMouseDelta = Vector2.SmoothDamp(currenMouseDelta, targetMouseDelta, ref currenMouseDeltaVelocity, lookSmoothTime);

        cameraPitch -= currenMouseDelta.y*mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currenMouseDelta.x*mouseSensitivity);
    }
    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(move);

                }



            }

            velocityY = 0.0f;
            if (Input.GetKey(KeyCode.Space))
            {
                velocityY += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            }
        }
        velocityY += gravity * Time.deltaTime;
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x)*walkSpeed+Vector3.up*velocityY;
        controller.Move(velocity*Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("FinishLine"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
