using UnityEngine;

public class RadiusControl : MonoBehaviour
{
    public Vector3 maxVertex;
    GameObject tool;

    //private float initialRadius = 1.0f; // Initial radius of the cylinder
    public float scaleStep = 0.99f; // scale adjustment step
    private bool meshesIntersecting = false;
    private Vector3 originalScale;


    void Start()
    {
        tool = GameObject.Find("Lathe Machine - tool-1"); // Reference to the tool GameObject
                                                          // Store the original scale of the cylinder
        originalScale = transform.localScale;
    }

    void Update()
    {

        //GetMaxZVertex();

        //Debug.Log("tool is at " + tool.transform.position);
        //Debug.Log("Cylinder is at " + cylinderObject.transform.position);

        // Check if the Z-coordinates of the tool and the cylinder are equal
        if (isIntersecting())
        {
            UpdateCylinderScale();
        }
    }



    void UpdateCylinderScale()
    {
        // Update the cylinder scale
        //Debug.Log("UPDATING");
        transform.localScale = new Vector3(transform.localScale.x * scaleStep, originalScale.y, transform.localScale.z * scaleStep);
    }


    bool isIntersecting()
    {
        GameObject otherObject = GameObject.Find("Lathe Machine - tool-1"); // Replace with the actual GameObject name

        if (otherObject != null)
        {
            
            // Get the MeshColliders of both GameObjects
            MeshCollider meshCollider1 = GetComponent<MeshCollider>();
            MeshCollider meshCollider2 = otherObject.GetComponent<MeshCollider>();

            if (meshCollider1 != null && meshCollider2 != null)
            {
                // Check for overlapping colliders
                Vector3 direction;
                float distance;
                meshesIntersecting = Physics.ComputePenetration(
                    meshCollider1, meshCollider1.transform.position, meshCollider1.transform.rotation,
                    meshCollider2, meshCollider2.transform.position, meshCollider2.transform.rotation,
                    out direction, out distance);

                if (meshesIntersecting)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else return false;
        }
        else return false;
    }
}