    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CreateCylinder : MonoBehaviour
    {
        public Material cylinderMaterial; // Public variable for attaching a material in the Inspector

        private GameObject cylinderObject;
        private float initialRadius = 3f; // Initial radius of the cylinder
        GameObject otherObject;

        // Start is called before the first frame update
        void Start()
        {
            CreateNewCylinder();
            RadiusControl customScript = cylinderObject.AddComponent<RadiusControl>();
            otherObject = GameObject.Find("Lathe Machine - tool-1"); // Replace with the actual GameObject name
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("tool position is " + (otherObject.transform.position));
            //Debug.Log("cylinder position is " + transform.position);
        }

        void CreateNewCylinder()
        {
            // Create a new cylinder GameObject
            cylinderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            // Remove the default capsule collider (if any)
            Destroy(cylinderObject.GetComponent<CapsuleCollider>());

            // Add a MeshCollider to the cylinder
            MeshCollider meshCollider = cylinderObject.AddComponent<MeshCollider>();

            // Set the parent to the GameObject that the script is attached to
            cylinderObject.transform.SetParent(transform);

            // Set the local scale and initial color of the cylinder
            cylinderObject.transform.localScale = new Vector3(initialRadius * 0.3f, 0.01f, initialRadius * 0.3f);
            //SetRandomColor();

            // Set the position as per your requirements
            cylinderObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (zPositionDecider() * 0.0035f));

            // Set the name of the cylinder as (name of parent) + " (1)"
            cylinderObject.name = transform.name + " (1)";

            // Rotate the cylinder to align its axis with the Z-axis
            cylinderObject.transform.Rotate(Vector3.right, 90.0f);

            // Assign the specified material to the cylinder renderer
            Renderer cylinderRenderer = cylinderObject.GetComponent<Renderer>();
            if (cylinderRenderer != null && cylinderMaterial != null)
            {
                cylinderRenderer.material = cylinderMaterial;
            }

            //Debug.Log("CYLINDER CREATED");
        }

        void SetRandomColor()
        {
            // Set a random color to the cylinder
            Renderer cylinderRenderer = cylinderObject.GetComponent<Renderer>();
            if (cylinderRenderer != null)
            {
                cylinderRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }

        int zPositionDecider()
        {
            // Access the substring from index 16 to the index before the last one of the GameObject's name and convert it to an integer
            if (gameObject.name.Length > 16)
            {
                string substring = gameObject.name.Substring(16, gameObject.name.Length - 17); // Extract the substring
                if (int.TryParse(substring, out int convertedValue))
                {
                    // Successfully parsed the substring into an integer
                    // Debug.Log("Substring as integer: " + convertedValue);
                    return convertedValue;
                }
            }

            // Default return value if the substring couldn't be parsed or the name length is less than 16
            return -1;
        }

    }
