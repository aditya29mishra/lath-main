using UnityEngine;

public class LeverControlledRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.forward; // Z-axis rotation
    public float rotationSpeed = 90f; // Degrees per second

    private bool isRotating = false;

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
        }
    }

    // Call this to start rotation
    public void StartRotation()
    {
        isRotating = true;
    }

    // Call this to stop rotation
    public void StopRotation()
    {
        isRotating = false;
    }

    // Or toggle rotation with this method
    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }
}
