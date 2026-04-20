using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // 1. Added namespace for the Input System

public class Glass_Door_Interaction : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private InputActionProperty triggerAction; // 2. Field to assign the Right Trigger

    [Header("Door Settings")]
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;

    void Start()
    {
        _closedRotation = transform.rotation;
        // Calculate the open rotation based on current starting rotation
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void Update()
    {
        // 3. Check if the trigger was pressed this frame
        if (triggerAction.action.WasPressedThisFrame())
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}