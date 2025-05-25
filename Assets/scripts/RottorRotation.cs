using UnityEngine;

public class RottorRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.forward; // Default Z-axis rotation
    public float slowSpeed = 45f;
    public float fastSpeed = 180f;
    public bool useFastSpeed = false;
    public bool clockwiseRotation = true;

    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            float currentSpeed = useFastSpeed ? fastSpeed : slowSpeed;
            float direction = clockwiseRotation ? 1f : -1f;

            transform.Rotate(rotationAxis.normalized * currentSpeed * direction * Time.deltaTime);
        }
    }

    // Start rotation
    public void StartRotation()
    {
        isRotating = true;
    }

    // Stop rotation
    public void StopRotation()
    {
        isRotating = false;
    }

    // Toggle rotation on/off
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    // Toggle between slow and fast speed
    public void ToggleSpeed()
    {
        useFastSpeed = !useFastSpeed;
        Debug.Log("Speed Mode: " + (useFastSpeed ? "Fast" : "Slow"));
    }

    // Toggle rotation direction
    public void ToggleDirection()
    {
        clockwiseRotation = !clockwiseRotation;
        Debug.Log("Rotation Direction: " + (clockwiseRotation ? "Clockwise" : "Anticlockwise"));
    }

    // Optional: Set speed mode directly
    public void SetSpeedMode(bool fast)
    {
        useFastSpeed = fast;
    }

    // Optional: Set rotation direction directly
    public void SetRotationDirection(bool clockwise)
    {
        clockwiseRotation = clockwise;
    }
}
