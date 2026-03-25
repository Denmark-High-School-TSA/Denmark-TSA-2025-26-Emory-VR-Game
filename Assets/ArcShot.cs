using UnityEngine;

public class ArcShot : MonoBehaviour
{
    public Transform rim;
    public float arcHeight = 3f;
    public float shotForce = 8f;

    public void Launch()
    {
        bool makeShot = Random.value <= 0.7f;

        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 targetPos = rim.position;

        if (!makeShot)
        {
            targetPos += new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
        }

        Vector3 velocity = CalculateArcVelocity(transform.position, targetPos, arcHeight);
        rb.linearVelocity = velocity;
    }

    Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float height)
    {
        float gravity = Physics.gravity.y;
        Vector3 displacement = end - start;

        Vector3 displacementXZ = new Vector3(displacement.x, 0, displacement.z);

        float time = Mathf.Sqrt(-2 * height / gravity) +
                     Mathf.Sqrt(2 * (displacement.y - height) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY;
    }
}