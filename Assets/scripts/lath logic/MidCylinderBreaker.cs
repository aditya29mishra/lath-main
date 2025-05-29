using UnityEngine;

public class MidCylinderBreaker : MonoBehaviour
{
    private GameObject tool1;

    private bool toolBroken = false;

    void Start()
    {
        tool1 = GameObject.Find("Lathe Machine - tool-1");
    }

    void Update()
    {
        if (tool1 == null || toolBroken) return;

        MeshCollider midCollider = GetComponent<MeshCollider>();
        MeshCollider toolCollider = tool1.GetComponent<MeshCollider>();

        if (midCollider != null && toolCollider != null)
        {
            bool isIntersecting = Physics.ComputePenetration(
                midCollider, transform.position, transform.rotation,
                toolCollider, tool1.transform.position, tool1.transform.rotation,
                out _, out _
            );

            if (isIntersecting)
            {
                Rigidbody rb = tool1.GetComponent<Rigidbody>();
                if (rb == null) rb = tool1.AddComponent<Rigidbody>();

                rb.useGravity = true;
                rb.isKinematic = false;

                toolBroken = true;
                Debug.Log("Tool-1 broke on MidCylinder contact.");
            }
        }
    }
}
