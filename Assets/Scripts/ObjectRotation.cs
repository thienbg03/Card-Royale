using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotation
    private float mouseX;
    private float mouseY;

    void Update()
    {
        // Ensure the player's input system or aiming condition is active
        if (PlayerManager.Instance != null && PlayerManager.Instance.InputTPC.isAiming)
        {
            // Get mouse input
            mouseX = Input.GetAxis("Mouse X"); // Horizontal mouse movement
            mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse movement

            // Rotate object based on mouse input
            RotateObject();
        }
    }

    void RotateObject()
    {
        // Rotate around the Y-axis (horizontal rotation)
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);

        // Rotate around the X-axis (vertical rotation)
        transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
