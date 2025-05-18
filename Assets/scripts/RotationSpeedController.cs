using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationSpeedController : MonoBehaviour
{
    public LeverControlledRotation targetRotationScript; // Assign in Inspector
    public void UpdateRotationSpeed(float value)
    {
        if (targetRotationScript != null)
        {
            // Map slider value to desired speed range (e.g., 0 to 360)
            float mappedSpeed = Mathf.Lerp(0f, 360f, value);
            targetRotationScript.rotationSpeed = mappedSpeed;
        }
    }
}
