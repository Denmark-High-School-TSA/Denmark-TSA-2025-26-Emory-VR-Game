using UnityEngine;
using UnityEngine.XR;

public class SprintOnLeftGrip : MonoBehaviour
{
    [Header("Drag Locomotion → Move here")]
    public MonoBehaviour moveProvider;

    public float normalSpeed = 1f;
    public float sprintSpeed = 3f;

    private InputDevice leftHand;

    void Start()
    {
        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    void Update()
    {
        if (!leftHand.isValid)
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            return;
        }

        bool gripPressed =
            leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool pressed) && pressed;

        // 🔑 moveSpeed is a PROPERTY, not a field
        var prop = moveProvider.GetType().GetProperty("moveSpeed");
        if (prop != null)
        {
            prop.SetValue(moveProvider, gripPressed ? sprintSpeed : normalSpeed);
        }
    }
}