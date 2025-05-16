using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public GameObject feedWheel1;
    private Quaternion initialRotation;
    public float translationSpeed = 0.00004f;

    public Axis rotationAxis = Axis.X;
    public Vector3 movementAxis = Vector3.right;

    public enum Axis { X, Y, Z }

    void Start()
    {
        if (feedWheel1 != null)
        {
            initialRotation = feedWheel1.transform.rotation;
        }
        else
        {
            Debug.LogError("feedWheel1 is not assigned!");
        }
    }

    void Update()
    {
        if (feedWheel1 == null) return;

        Quaternion rotationChange = feedWheel1.transform.rotation * Quaternion.Inverse(initialRotation);
        Vector3 eulerRotationChange = rotationChange.eulerAngles;

        float angle = 0f;
        switch (rotationAxis)
        {
            case Axis.X: angle = eulerRotationChange.x; break;
            case Axis.Y: angle = eulerRotationChange.y; break;
            case Axis.Z: angle = eulerRotationChange.z; break;
        }

        if (angle > 300)
        {
            float translationAmount = (360 - angle) * translationSpeed;
            transform.Translate(movementAxis.normalized * translationAmount);
        }
        else
        {
            float translationAmount = angle * translationSpeed;
            transform.Translate(-movementAxis.normalized * translationAmount);
        }

        initialRotation = feedWheel1.transform.rotation;
    }
}
