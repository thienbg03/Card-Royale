using System.Collections;
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
            if (isSprinting && PlayerManager.Instance.stamina > 10){
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
        PlayerManager.Instance.animationManager.UpdateAnimatorPlayerSpeed(speed);
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

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.IsPerformingAction)
            return;

        if (PlayerManager.Instance.stamina < 35)
            return;

        PlayerManager.Instance.animationManager.PerformAnimationAction("Dodge", true, true);
        Debug.Log("dodge");
    }
    private void MoveCharacter()
    {
        if (!PlayerManager.Instance.CanMove)
            return;

        // Movement based on the direction of the camera
        verticalInput = inputVector.y;
        horizontalInput = inputVector.x;  
        movementDirection = PlayerCamera.transform.forward * verticalInput;
        movementDirection += PlayerCamera.transform.right * horizontalInput;
        movementDirection.Normalize(); // Normalize to prevent diagonal speed boosts
        movementDirection *= speed * Time.deltaTime; // Apply speed and frame rate adjustment
        movementDirection = new Vector3 (movementDirection.x, movementDirection.y - movementDirection.y, movementDirection.z);
        transform.position += movementDirection; // Move the character
    }

    private void RotateCharacter()
    {
       if (!PlayerManager.Instance.CanRotate)
            return;

       Quaternion newRotation = Quaternion.LookRotation(movementDirection);
       Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed* Time.deltaTime);
       transform.rotation = targetRotation;
    }

    public void DepleteStamina(float amount)
    {
        PlayerManager.Instance.stamina -= amount;
        PlayerManager.Instance.stamina = Mathf.Clamp(PlayerManager.Instance.stamina, 0, 100);
        print("Stamina Depleted: " + PlayerManager.Instance.stamina);
        if(PlayerManager.Instance.stamina <= 0)
        {
            StartCoroutine(StaminaDepleted());
        }
    }

    public void AddStamina(float amount)
    {
        if (!PlayerManager.Instance.CanAddStamina)
        {
            return;
        }
        PlayerManager.Instance.stamina += amount;
        PlayerManager.Instance.stamina = Mathf.Clamp(PlayerManager.Instance.stamina, 0, 100);
        print("Stamina Added: " + PlayerManager.Instance.stamina);
    }

    private IEnumerator StaminaDepleted()
    {
        print("TIREDDD");
       PlayerManager.Instance.CanAddStamina = false;
       yield return new WaitForSeconds(3f);
       PlayerManager.Instance.CanAddStamina = true;
    }
}
