using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCylinder : MonoBehaviour
{
    public Material cylinderMaterial; // Material for outer cylinder
    public Material innerMaterial;    // Material for inner cylinder (hole)

    private GameObject outerCylinder;
    private GameObject innerCylinder;

    private float initialRadius = 3f;

    void Start()
    {
        CreateOuterCylinder();
        CreateInnerCylinder();

        RadiusControl radiusControl = outerCylinder.AddComponent<RadiusControl>();
        radiusControl.innerCylinder = innerCylinder;
    }

    void CreateOuterCylinder()
    {
        outerCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(outerCylinder.GetComponent<CapsuleCollider>());
        MeshCollider meshCollider = outerCylinder.AddComponent<MeshCollider>();

        outerCylinder.transform.SetParent(transform);
        outerCylinder.transform.localScale = new Vector3(initialRadius * 0.3f, 0.01f, initialRadius * 0.3f);
        outerCylinder.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (zPositionDecider() * 0.0035f));
        outerCylinder.name = transform.name + "_OuterCylinder";
        outerCylinder.transform.Rotate(Vector3.right, 90.0f);

        if (cylinderMaterial != null)
        {
            Renderer renderer = outerCylinder.GetComponent<Renderer>();
            renderer.material = cylinderMaterial;
        }
    }

    void CreateInnerCylinder()
    {
        innerCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(innerCylinder.GetComponent<CapsuleCollider>());
        MeshCollider meshCollider = innerCylinder.AddComponent<MeshCollider>();
        meshCollider.convex = true;

        innerCylinder.transform.SetParent(outerCylinder.transform);
        innerCylinder.transform.localPosition = Vector3.zero;
        innerCylinder.transform.localRotation = Quaternion.identity;

        // Same Y scale as outer, but start with very small diameter (X and Z)
        innerCylinder.transform.localScale = new Vector3(0.01f, 1.1f, 0.01f);
        innerCylinder.name = "InnerCylinder";

        if (innerMaterial != null)
        {
            Renderer renderer = innerCylinder.GetComponent<Renderer>();
            renderer.material = innerMaterial;
        }
        else
        {
            innerCylinder.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    int zPositionDecider()
    {
        if (gameObject.name.Length > 16)
        {
            string substring = gameObject.name.Substring(16, gameObject.name.Length - 17);
            if (int.TryParse(substring, out int convertedValue))
                return convertedValue;
        }
        return -1;
    }
}
