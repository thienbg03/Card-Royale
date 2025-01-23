using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField] private GameObject arrowPrefab; // Arrow prefab
    [SerializeField] private Transform arrowSpawnPoint; // Where the arrow spawns
    [SerializeField] private float arrowSpeed = 20f; // Speed of the arrow
    private bool isAttacking;
    public bool isAiming;
    public Camera PlayerCamera;
    public Image crosshair;
    // Flag to track input
    private Vector2 inputVector;
    public GameObject FakeArrow;

    void Start()
    {
        walkingSpeed = 2f;
        runningSpeed = 4f;
        speed = 0;
        rotationSpeed = 8f;
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


        if (PlayerManager.Instance.PlayerState == PlayerState.Bow)
        {
            if (isAiming) {
                BowAiming();
            }
            else
            {
                crosshair.enabled = false;
                FakeArrow.SetActive(false);
            }
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Store the input vector when the input action is called
        inputVector = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) // the key has been pressed
        {
            isSprinting = true;
        }
        if (context.canceled) //the key has been released
        {
            isSprinting = false;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.IsPerformingAction)
            return;

        if (PlayerManager.Instance.stamina < 35)
            return;

        PlayerManager.Instance.animationManager.PerformAnimationAction("Dodge", true, true);
    }

    public void OnAttack()
    {
        if (isAiming)
        {
            ShootArrow();
        }

        if (PlayerManager.Instance.CanDoCombo)
        {
            PlayerManager.Instance.animationManager.UpdateAnimatorAttackCombo(true);
            PlayerManager.Instance.CanDoCombo = false;
            PlayerManager.Instance.animationManager.PerformAnimationAction("Slash2", true);
            return;
        }

        if (PlayerManager.Instance.IsPerformingAction)
        {
            return;
        }

        if (PlayerManager.Instance.PlayerState == PlayerState.Sword)
        {
            RotateCharacterForward();
            PlayerManager.Instance.animationManager.PerformAnimationAction("Slash1", true);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.PlayerState != PlayerState.Bow)
            return;

        if (context.performed) // the key has been pressed
        {
            PlayerManager.Instance.animationManager.PerformAnimationAction("BowDraw", true);
            PlayerManager.Instance.animationManager.UpdateAnimationParamter("IsAiming", true);
            PlayerManager.Instance.SwitchToAimingCamera();
            isAiming = true;
        }
        if (context.canceled) //the key has been released
        {
            isAiming = false;
            PlayerManager.Instance.animationManager.UpdateAnimationParamter("IsAiming", true);
            PlayerManager.Instance.SwitchToDefaultCamera();
        }
    }

    private void ShootArrow()
    {
        // Instantiate the arrow at the spawn point
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
  
        // Get the Rigidbody component of the arrow
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate the direction to shoot the arrow
            Vector3 shootDirection = PlayerManager.Instance.playerAimCamera.transform.forward;
            rb.linearVelocity = shootDirection * arrowSpeed; // Apply velocity to the arrow
            print("SHOOTING");
        }
        // Reset aiming state
        ResetAiming();
    }
    private void ResetAiming()
    {
        isAiming = false; 
        PlayerManager.Instance.animationManager.UpdateAnimationParamter("IsAiming", false);
        PlayerManager.Instance.SwitchToDefaultCamera();
        crosshair.enabled = false;
    }

    private void BowAiming()
    {
        //Rotate Character with Camera
        Vector3 cameraForward = PlayerManager.Instance.playerAimCamera.transform.forward;
        cameraForward.y = 0f; // Keep the forward direction on the horizontal plane
        cameraForward.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        crosshair.enabled = true;

        FakeArrow.SetActive(true);
    }

    private void MoveCharacter()
    {
        if (!PlayerManager.Instance.CanMove)
            return;


        verticalInput = inputVector.y;
        horizontalInput = inputVector.x;
        movementDirection = PlayerCamera.transform.forward * verticalInput + PlayerCamera.transform.right * horizontalInput;
        movementDirection.y = 0f;
        movementDirection.Normalize();
        Vector3 movement = movementDirection * speed * Time.deltaTime;
        transform.position += movement; ;
    }


    private void RotateCharacter()
    {
       if (!PlayerManager.Instance.CanRotate)
            return;
       
        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RotateCharacterForward()
    {
        if (!PlayerManager.Instance.CanRotate)
            return;

        Quaternion newRotation = Quaternion.LookRotation(PlayerCamera.transform.forward);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, PlayerCamera.transform.forward, rotationSpeed, 0.0f);

        newDirection = new Vector3 (newDirection.x, 0, newDirection.z);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }

    public void DepleteStamina(float amount)
    {
        PlayerManager.Instance.stamina -= amount;
        PlayerManager.Instance.stamina = Mathf.Clamp(PlayerManager.Instance.stamina, 0, 100);
        PlayerManager.Instance.PlayerUIManager.SetStamina(PlayerManager.Instance.stamina);
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
        PlayerManager.Instance.PlayerUIManager.SetStamina(PlayerManager.Instance.stamina);
    }

    private IEnumerator StaminaDepleted()
    {
       PlayerManager.Instance.CanAddStamina = false;
       yield return new WaitForSeconds(3f);
       PlayerManager.Instance.CanAddStamina = true;
    }

    public void UpdateCanDoCombo()
    {
        PlayerManager.Instance.CanDoCombo = true;
    }

    
}
