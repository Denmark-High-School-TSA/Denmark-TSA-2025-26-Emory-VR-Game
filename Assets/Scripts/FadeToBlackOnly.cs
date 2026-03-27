using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlackOnly : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Drag your Black UI Image here")]
    public Image cameraBlackScreen; 
    
    [Tooltip("How long the fade takes in seconds")]
    public float fadeDuration = 2f;

    private bool _isFading = false;

    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    public IEnumerator FadeFromBlack()
    {
        // Start fully black
        Color color = cameraBlackScreen.color;
        color.a = 1f;
        cameraBlackScreen.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            cameraBlackScreen.color = color;
            yield return null;
        }

        color.a = 0f;
        cameraBlackScreen.color = color;
    }

    public void TriggerFade()
    {
        Debug.Log("<color=green>SUCCESS: The Button was physically pressed!</color>");

        if (cameraBlackScreen == null)
        {
            Debug.LogError("ERROR: 'Camera Black Screen' is empty in the Inspector! Drag your Black Image into the slot.");
            return;
        }

        if (!_isFading)
        {
            _isFading = true;
            StartCoroutine(FadeRoutine());
        }
        else
        {
            Debug.Log("Fade is already in progress...");
        }
    }

    public IEnumerator FadeRoutine()
    {
        Debug.Log("STARTING ANIMATION: Fading to black now...");
        
        float timer = 0f;
        Color color = cameraBlackScreen.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            cameraBlackScreen.color = color;
            yield return null; 
        }

        color.a = 1f;
        cameraBlackScreen.color = color;
        
        Debug.Log("<color=cyan>FADE FINISHED: The screen should be completely black now.</color>");
    }
}