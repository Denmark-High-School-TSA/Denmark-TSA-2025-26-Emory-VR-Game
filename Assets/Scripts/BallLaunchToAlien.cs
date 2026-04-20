using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public Transform alienTarget;
    public Animator alienAnimator;
    public Transform secondSpawnPoint; // New: Second ball spawn location
    public Transform rimTarget; // New: Target point on the rim

    public float launchSpeed = 11f;
    public float upwardForce = 5.5f;
    public float spawnInterval = 4f;
    public float ballLifeTime = 0.85f;
    public float initialDelay = 5f;
    public float animationDelay = 0.5f;
    public float shotAccuracy = 70f; // New: 70% success rate
    public float arcHeight = 3f; // New: Height of the arc trajectory
    public float ballDrag = 1f;
    
    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval);
    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        Vector3 direction = (alienTarget.position - spawnPoint.position).normalized;

        rb.linearVelocity = direction * launchSpeed;
        rb.linearVelocity += Vector3.up * upwardForce;

        Destroy(ball, ballLifeTime);
        // Add delay before animation
        StartCoroutine(PlayAnimationWithDelay(ball));
    }

    IEnumerator PlayAnimationWithDelay(GameObject ball)
    {
        yield return new WaitForSeconds(animationDelay);

        if (alienAnimator != null)
        {
            alienAnimator.SetTrigger("Shoot");
        }

        // Spawn second ball after animation
        yield return new WaitForSeconds(0.3f);
        SpawnSecondBall();
    }

    void SpawnSecondBall()
    {
        GameObject secondBall = Instantiate(ballPrefab, secondSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = secondBall.GetComponent<Rigidbody>();

        rb.linearDamping = ballDrag;
        // 70% chance to make the shot
        bool makeShot = Random.Range(0f, 100f) < shotAccuracy;

        Vector3 targetPosition = rimTarget.position;

        // If not making the shot, offset the target slightly
        if (!makeShot)
        {
            targetPosition += new Vector3(
                Random.Range(-0.6f, 0.6f),
                Random.Range(-0.4f, 0.4f),
                Random.Range(-0.6f, 0.6f)
            );
        }

        // Calculate arc velocity to hit target with specified height
        Vector3 direction = CalculateArcVelocity(secondSpawnPoint.position, targetPosition, arcHeight);
        rb.linearVelocity = direction;

        Destroy(secondBall, 3f);
    }

    Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float height)
    {
        float gravity = Physics.gravity.y;

        float h1 = start.y;
        float h2 = end.y;
        float h = height;

        // Calculate time to reach target
        float time = Mathf.Sqrt(2 * h / Mathf.Abs(gravity));

        // Calculate horizontal velocity
        float vx = (end.x - start.x) / time;
        float vz = (end.z - start.z) / time;

        // Calculate vertical velocity
        float vy = Mathf.Sqrt(2 * Mathf.Abs(gravity) * h);

        // Adjust for height difference
        if (h2 > h1)
        {
            vy += Mathf.Sqrt(2 * Mathf.Abs(gravity) * (h2 - h1));
        }
        else
        {
            vy -= Mathf.Sqrt(2 * Mathf.Abs(gravity) * (h1 - h2));
        }

        return new Vector3(vx, vy, vz);
    }
}