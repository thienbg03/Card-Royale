using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputTPC : MonoBehaviour
{
    // Variables for movement
    [SerializeField] private float moveAmount;
    [SerializeField] private float verticalInput;
    [SerializeField] private float horizontalInput;
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float speed;
    [SerializeField] private bool isSprinting;
    [SerializeField] private float rotationSpeed;

    public Camera PlayerCamera;

    // Flag to track input
    private Vector2 inputVector;

    void Start()
    {
        walkingSpeed = 1f;
        runningSpeed = 3f;
        speed = 0;
        rotationSpeed = 8f;
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputVector != Vector2.zero)
        {
            if (isSprinting){
                speed = runningSpeed;
            }
            else
            {
                speed = walkingSpeed;
            }
            MoveCharacter();
            RotateCharacter();
        }
        else
        {
            speed = 0f;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Store the input vector when the input action is called
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.action.IsPressed();
    }
    private void MoveCharacter()
    {
        // Movement based on the direction of the camera
        verticalInput = inputVector.y;
        horizontalInput = inputVector.x;  
        movementDirection = PlayerCamera.transform.forward * verticalInput;
        movementDirection += PlayerCamera.transform.right * horizontalInput;
        movementDirection.Normalize(); // Normalize to prevent diagonal speed boosts
        movementDirection *= speed * Time.deltaTime; // Apply speed and frame rate adjustment
        transform.position += movementDirection; // Move the character
    }

    private void RotateCharacter()
    {
       Quaternion newRotation = Quaternion.LookRotation(movementDirection);
       Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed* Time.deltaTime);
       transform.rotation = targetRotation;
    }
}
