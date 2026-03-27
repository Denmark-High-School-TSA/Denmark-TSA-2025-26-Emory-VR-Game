using JetBrains.Annotations;
using UnityEngine;

public class PaddleCollision : MonoBehaviour
{
    public GameObject Paddle;
    public GameObject ball;
    public Transform RightHandTransform;


    private Rigidbody PaddleRb;
    private Vector3 LastControllerPosition;
    private void OnTriggerEnter(Collider other)
    {


        float pushForce = 10f;

        Rigidbody ballRb = other.GetComponent<Rigidbody>();

        // Push ball out of paddle when clipping
        if (PaddleRb != null)
        {
            Vector3 direction = (other.transform.position - LastControllerPosition).normalized;
            ballRb.AddForce(-direction * pushForce, ForceMode.Impulse);
        }
    }

    public Vector3 getLastControllerPosition()
    {
        return LastControllerPosition;
    }
}