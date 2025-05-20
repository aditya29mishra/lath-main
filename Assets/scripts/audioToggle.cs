using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioToggle : MonoBehaviour
{
    public string targetTag = "Activator"; // Set this to the tag you want to respond to (e.g., "Activator")
    public GameObject Audio;  // Assign in Inspector
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (Audio != null)
            {
                Debug.Log("Activating ");
                Audio.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (Audio != null)
            {
                Debug.Log("Deactivating ");
                Audio.SetActive(false);
            }
        }
    }
}
