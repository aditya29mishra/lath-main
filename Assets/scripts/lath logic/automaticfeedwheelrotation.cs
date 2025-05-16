using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class automaticfeedwheelrotation : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 90f;
    //private bool isKnobPulled = false;
    private Coroutine rotationCoroutine;

    void Update()
    {

    }



    public void StartWheelRotation()
    {
        if (rotationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotateWheel());
        }
    }

    public void StopWheelRotation()
    {
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    IEnumerator RotateWheel()
    {
        while (true) // Continuously rotate while the knob is pulled
        {
            // Rotate the wheel continuously
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);

            yield return null; // Wait for the next frame
        }
    }
}
