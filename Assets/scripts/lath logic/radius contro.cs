using UnityEngine;

public class RadiusControl : MonoBehaviour
{
    public GameObject innerCylinder;
    private GameObject tool1, tool2, tool3;

    public float turnStep = 0.99f;    // Cutting from outside (scale down)
    public float drillStep = 0.01f;   // Drilling - increase hole diameter (X,Z of inner)
    public float boreStep = 0.01f;    // Boring - increase hole diameter (X,Z of inner)

    private Vector3 originalOuterScale;

    void Start()
    {
        tool1 = GameObject.Find("Lathe Machine - tool-1");
        tool2 = GameObject.Find("Lathe Machine - tool-2");
        tool3 = GameObject.Find("Lathe Machine - tool-3");

        originalOuterScale = transform.localScale;
    }

    void Update()
    {
        if (IsIntersecting(tool1))
            PerformTurning();

        if (IsIntersecting(tool2))
            PerformDrilling();

        if (IsIntersecting(tool3))
            PerformBoring();
    }

    void PerformTurning()
    {
        transform.localScale = new Vector3(transform.localScale.x * turnStep, originalOuterScale.y, transform.localScale.z * turnStep);
    }

    void PerformDrilling()
    {
        Vector3 scale = innerCylinder.transform.localScale;

        float maxDiameter = transform.localScale.x * 0.225f; // leave small margin

        if (scale.x + drillStep < maxDiameter && scale.z + drillStep < maxDiameter)
        {
            innerCylinder.transform.localScale = new Vector3(scale.x + drillStep, scale.y, scale.z + drillStep);
        }
    }

    void PerformBoring()
{
    if (tool3 == null || innerCylinder == null) return;

    MeshCollider outerMesh = GetComponent<MeshCollider>();
    MeshCollider toolMesh = tool3.GetComponent<MeshCollider>();

    if (outerMesh == null || toolMesh == null) return;

    Vector3 direction;
    float distance;

    if (Physics.ComputePenetration(
        outerMesh, transform.position, transform.rotation,
        toolMesh, tool3.transform.position, tool3.transform.rotation,
        out direction, out distance))
    {
        // Estimate target diameter based on penetration depth
        float estimatedRadius = distance * 5f; // tweak this multiplier
        float maxRadius = transform.localScale.x * 0.5f * 0.95f;

        estimatedRadius = Mathf.Min(estimatedRadius, maxRadius);
        estimatedRadius = Mathf.Max(estimatedRadius, 0.01f); // prevent zero

        innerCylinder.transform.localScale = new Vector3(estimatedRadius * 2, innerCylinder.transform.localScale.y, estimatedRadius * 2);
    }
}


    bool IsIntersecting(GameObject tool)
    {
        if (tool == null) return false;

        MeshCollider mesh1 = GetComponent<MeshCollider>();
        MeshCollider mesh2 = tool.GetComponent<MeshCollider>();

        if (mesh1 != null && mesh2 != null)
        {
            return Physics.ComputePenetration(
                mesh1, transform.position, transform.rotation,
                mesh2, tool.transform.position, tool.transform.rotation,
                out _, out _
            );
        }
        return false;
    }
}
