using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public Transform targetPoint;

    public float launchForce = 8f;
    public float upwardForce = 3f;
    public float spawnInterval = 5f;

    public float randomHorizontalOffset = 1f;
    public float randomUpwardOffset = 1f;

    private GameObject currentBall;

    void Start()
    {
        InvokeRepeating(nameof(LaunchBall), 1f, spawnInterval);
    }

    void LaunchBall()
    {
        // Destroy previous ball before spawning new one
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();

        // Slight random variation (still catchable)
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomHorizontalOffset, randomHorizontalOffset),
            Random.Range(0, randomUpwardOffset),
            Random.Range(-randomHorizontalOffset, randomHorizontalOffset)
        );

        Vector3 direction = (targetPoint.position + randomOffset - spawnPoint.position).normalized;

        rb.linearVelocity = direction * launchForce;
        rb.linearVelocity += Vector3.up * upwardForce;
    }
}