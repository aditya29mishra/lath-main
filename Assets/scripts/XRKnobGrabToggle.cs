using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRKnobGrabToggle : MonoBehaviour
{
    private XRKnob knob;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        knob = GetComponent<XRKnob>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(_ => knob.enabled = false);
        grabInteractable.selectExited.AddListener(_ => knob.enabled = true);
    }
}
