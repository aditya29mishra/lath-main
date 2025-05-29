using UnityEngine;
using System.Collections;

public class RadiusControl : MonoBehaviour
{
    public GameObject innerCylinder;
    private GameObject tool1, tool2, tool3;
    public GameObject tool1Audio;
    private AudioSource audioClip;

    public float turnStep = 0.99f;
    public float drillStep = 0.01f;
    public float boreStep = 0.01f;

    private Vector3 originalOuterScale;

    private Coroutine audioCoroutine;

    private bool isTurning = false;
    public float turningDelay = 0.2f; // Adjust delay in seconds
    public float audioDelay = 1f; // Adjust audio delay in seconds


    void Start()
    {
        tool1 = GameObject.Find("Lathe Machine - tool-1");
        tool2 = GameObject.Find("Lathe Machine - tool-2");
        tool3 = GameObject.Find("Lathe Machine - tool-3");

        tool1Audio = GameObject.Find("Tool1Audio");
        if (tool1Audio != null)
        {
            tool1Audio.SetActive(true);
        }

        audioClip = tool1Audio.GetComponent<AudioSource>();
        originalOuterScale = transform.localScale;
    }

    void Update()
    {
        bool isTool1Intersecting = IsIntersecting(tool1);
        bool isTool2Intersecting = IsIntersecting(tool2);
        bool isTool3Intersecting = IsIntersecting(tool3);

        if (isTool1Intersecting)
        {
            if (audioClip != null && !audioClip.isPlaying)
                StartCoroutine(EnableAudioForOneFrame());

            if (!isTurning)
                StartCoroutine(DelayedTurning());
        }

        if (isTool2Intersecting)
        {
            PerformDrilling();
        }

        if (isTool3Intersecting)
        {
            PerformBoring();
        }
    }

    IEnumerator EnableAudioForOneFrame()
    {
        audioClip.enabled = true;
        yield return new WaitForSeconds(audioDelay);
        audioClip.enabled = false;
    }

    IEnumerator DelayedTurning()
    {
        isTurning = true;
        yield return new WaitForSeconds(turningDelay);

        // Re-check tool contact before turning
        if (IsIntersecting(tool1))
        {
            PerformTurning();
        }

        isTurning = false;
    }


    void PerformTurning()
    {
        transform.localScale = new Vector3(transform.localScale.x * turnStep, originalOuterScale.y, transform.localScale.z * turnStep);
        StartCoroutine(SpawnEffectRepeatedly());
    }

    IEnumerator SpawnEffectRepeatedly()
    {
        GameObject effect = GameObject.Find("effect");

        if (effect != null && effect.transform.childCount > 0)
        {
            Transform childTransform = effect.transform.GetChild(0);
            GameObject childEffect = childTransform.gameObject;

            bool wasInitiallyInactive = !childEffect.activeSelf;
            childEffect.SetActive(true); // Activate to allow instantiation

            for (int i = 0; i < 1; i++) // Adjust number of duplicates
            {
                GameObject spawned = Instantiate(childEffect, transform.position, Quaternion.identity);
                spawned.transform.localScale = new Vector3(0.260006577f, 0.260006577f, 0.0660208687f);
                yield return new WaitForSeconds(0.5f);
            }

            if (wasInitiallyInactive)
            {
                childEffect.SetActive(false); // Restore original state
            }
        }
    }


    void PerformDrilling()
    {
        Vector3 scale = innerCylinder.transform.localScale;
        float maxDiameter = transform.localScale.x * 0.3f;

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
            float estimatedRadius = distance * 5f;
            float maxRadius = transform.localScale.x * 0.5f * 0.95f;

            estimatedRadius = Mathf.Clamp(estimatedRadius, 0.01f, maxRadius);

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
