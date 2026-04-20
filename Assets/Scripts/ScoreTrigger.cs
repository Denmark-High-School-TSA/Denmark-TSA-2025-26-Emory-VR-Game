using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;
    private bool hasScored = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasScored) return;

        if (other.CompareTag("Basketball"))
        {
            scoreManager.AddScoreHuman(2);
        }
        else if (other.CompareTag("Basketball2"))
        {
            scoreManager.AddScoreAlien(2);
        }
        else
        {
            return;
        }

        if (audioSource != null)
            audioSource.Play();

        hasScored = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basketball") || other.CompareTag("Basketball2"))
        {
            hasScored = false;
        }
    }
}