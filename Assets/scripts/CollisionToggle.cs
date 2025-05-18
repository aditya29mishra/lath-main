using UnityEngine;

public class CollisionToggle : MonoBehaviour
{
    public GameObject objectToDeactivate;  // Assign in Inspector
    public GameObject objectToDeactivate1;  // Assign in Inspector

    public GameObject objectToActivate;    // Assign in Inspector
    public string targetTag = "Activator"; // Set this to the tag you want to respond to (e.g., "Activator")

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (objectToDeactivate != null)
            {
                Debug.Log("Deactivating " + objectToDeactivate.name);
                objectToDeactivate.SetActive(false);
                objectToDeactivate1.SetActive(false);
            }

            if (objectToActivate != null)
            {
                Debug.Log("Activating " + objectToActivate.name);
                objectToActivate.SetActive(true);
            }
        }
    }


}
