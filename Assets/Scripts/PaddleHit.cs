using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PaddleHit : MonoBehaviour
{

    public GameObject ball;
    public XRNode controllerNode = XRNode.RightHand;

    public float hitForceMultiplier = 1.5f;
    public float hitDistanceThreshold = 0.2f; // How close before we force a hit

    private Rigidbody paddleRb;
    private Rigidbody ballRb;
    private Vector3 controllerVelocity;

    void Start()
    {
        // Paddle setup
        paddleRb = GetComponent<Rigidbody>();
        paddleRb.isKinematic = true;
        paddleRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        if (ball != null)
        {
            ballRb = ball.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                ballRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
            else
            {
                Debug.LogError("Ball GameObject has no Rigidbody!");
            }
        }
        else
        {
            Debug.LogError("Ball GameObject not assigned in Inspector!");
        }
    }

    void Update()
    {
        // Get VR controller velocity from XR tracking
        InputDevices.GetDeviceAtXRNode(controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);

        // Extra check: if moving toward ball and close enough, apply bounce
        if (ballRb != null && controllerVelocity.magnitude > 0.01f)
        {
            Vector3 toBall = (ballRb.position - transform.position).normalized;
            float dot = Vector3.Dot(controllerVelocity.normalized, toBall);

            if (dot > 0.5f && Vector3.Distance(transform.position, ballRb.position) <= hitDistanceThreshold)
            {
                ApplyBounce(toBall);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && ballRb != null)
        {
            Vector3 toBall = (ballRb.position - transform.position).normalized;
            float dot = Vector3.Dot(controllerVelocity.normalized, toBall);

            if (dot > 0.1f) // Only bounce if moving toward ball
            {
                ApplyBounce(toBall);
            }
        }
    }

    private void ApplyBounce(Vector3 toBall)
    {
        Vector3 newVelocity = ballRb.linearVelocity + controllerVelocity * hitForceMultiplier;

        // Guarantee ball goes away from paddle
        if (Vector3.Dot(newVelocity, toBall) < 0)
        {
            newVelocity = Vector3.Reflect(newVelocity, -toBall);
        }

        ballRb.linearVelocity = newVelocity;
        Debug.Log($"Bounce applied — New velocity: {newVelocity}");
    }
}