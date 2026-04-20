using UnityEngine;
 
public class BallCollisionSound : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;          // drag 3 clips in Inspector
 
    [Header("Layer")]
    [SerializeField] private LayerMask soundLayers;
 
    void OnCollisionEnter(Collision collision)
    {
        if (clips.Length == 0) return;
 
        // Check if the object we hit is on the right layer
        if ((soundLayers.value & (1 << collision.gameObject.layer)) == 0) return;
 
        // Pick a random clip and play it
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip);
    }
}