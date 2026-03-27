using UnityEngine;
using UnityEngine.InputSystem;

public class BallGripSpawner : MonoBehaviour
{
    public InputActionProperty leftGripAction;
    public GameObject ball;              // existing ball in scene
    public Transform leftHandTransform;   // left controller or hold point
    public GameObject ServeText;
    private Rigidbody ballRb;
    private bool isHolding = false;
    private Vector3 lastHandPosition;
    private Vector3 handVelocity;

    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (leftGripAction.action == null) return;

        float gripValue = leftGripAction.action.ReadValue<float>();

        Vector3 currentHandPosition = leftHandTransform.position;

        handVelocity = (currentHandPosition - lastHandPosition) / Time.deltaTime;
        lastHandPosition = currentHandPosition;

        if (gripValue > 0.8f && !isHolding)
        {
            ServeText.SetActive(false);
            HoldBall();
        }
        else if (gripValue < 0.2f && isHolding)
        {
            ReleaseBall();
        }
    }

    void HoldBall()
    {
        isHolding = true;

        ballRb.isKinematic = true;
        ballRb.useGravity = false;

        ball.transform.SetParent(leftHandTransform);
        ball.transform.localPosition = Vector3.forward * 0.01f;

    }

    void ReleaseBall()
    {
        isHolding = false;

        ball.transform.SetParent(null);

        ballRb.isKinematic = false;
        ballRb.useGravity = true;
        ballRb.linearVelocity = handVelocity;
    }
}