using UnityEngine;
using UnityEngine.XR;


public class BasketballShootAssist : MonoBehaviour
{
    public Rigidbody ballRb;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    public Transform head; // Main Camera

    [Header("Shot Feel")]
    public float shotForce = 7f;
    public float arcBoost = 4f;
    public float timingWindow = 0.15f;
    public float missOffset = 1.2f;

    private bool isHeld;
    private bool isJumping;
    private float jumpPeakTime;

    private InputDevice rightController;
    private bool lastAState;

    private float lastHeadY;

    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        lastHeadY = head.position.y;

        grab.selectEntered.AddListener(_ => isHeld = true);
        grab.selectExited.AddListener(_ => isHeld = false);
    }

    void Update()
    {
        if (!rightController.isValid)
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!isHeld) return;

        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aPressed);

        float headY = head.position.y;
        float verticalDelta = (headY - lastHeadY) / Time.deltaTime;

        // Jump start
        if (verticalDelta > 0.2f)
            isJumping = true;

        // Jump peak
        if (isJumping && Mathf.Abs(verticalDelta) < 0.05f)
            jumpPeakTime = Time.time;

        // Shoot on A release during jump
        if (lastAState && !aPressed && isJumping)
            ShootBall();

        lastAState = aPressed;
        lastHeadY = headY;

        // Reset when falling
        if (verticalDelta < -0.2f)
            isJumping = false;
    }

    void ShootBall()
    {
        grab.enabled = false;
        ballRb.isKinematic = false;

        float timingError = Mathf.Abs(Time.time - jumpPeakTime);

        Vector3 shootDir = head.forward;
        Vector3 force = shootDir * shotForce + Vector3.up * arcBoost;

        if (timingError < timingWindow)
        {
            ballRb.AddForce(force, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 miss = new Vector3(
                Random.Range(-missOffset, missOffset),
                Random.Range(-missOffset * 0.5f, missOffset),
                0
            );

            ballRb.AddForce(force + miss, ForceMode.VelocityChange);
        }

        Invoke(nameof(ReEnableGrab), 0.2f);
    }

    void ReEnableGrab()
    {
        grab.enabled = true;
    }
}