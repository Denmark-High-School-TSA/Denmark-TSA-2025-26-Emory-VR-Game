using UnityEngine;

public class AlienShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public Animator animator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball"))
        {
            Invoke("ShootBall", 1f);
        }
    }

    void ShootBall()
    {
        GameObject newBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        animator.SetTrigger("ShootingAnimation");

        newBall.GetComponent<ArcShot>().Launch();
    }
}