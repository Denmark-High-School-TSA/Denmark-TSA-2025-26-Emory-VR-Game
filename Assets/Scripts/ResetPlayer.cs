using UnityEngine;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
 
public class ResetPlayer : MonoBehaviour
{
    [Header("References")]
    public GameObject resetMenu;
    public Transform  origin;
 
    [Header("Input")]
    public InputActionProperty leftTriggerAction;
 
    void Update()
    {
        // Only listen when the menu is visible
        if (!resetMenu.activeSelf) return;
 
        if (leftTriggerAction.action.WasPressedThisFrame())
        {
            ResetPos();
            resetMenu.SetActive(false);
        }
    }
 
    void ResetPos()
    {
        XROrigin xrOrigin = GetComponent<XROrigin>();
        if (xrOrigin == null)
        {
            Debug.LogWarning("ResetPlayer: no XROrigin found on this GameObject.");
            return;
        }
 
        Camera cam = xrOrigin.Camera;
 
        // Calculate the offset between the XR rig root and the camera
        // so we move the rig, not just the camera anchor
        Vector3 cameraOffset = cam.transform.position - xrOrigin.transform.position;
        cameraOffset.y = 0; // only correct for horizontal offset
 
        xrOrigin.transform.position = origin.position - cameraOffset;
        xrOrigin.transform.rotation = origin.rotation;
    }
}