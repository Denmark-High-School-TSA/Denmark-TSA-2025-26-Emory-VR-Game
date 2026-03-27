using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 1.0f;

    void Start()
    {
        // Always fade out (clear) when the scene loads
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        float targetAlpha = 1f;
        Color color = blackScreen.color;
        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            blackScreen.color = color;
            yield return null;
        }
        color.a = targetAlpha;
        blackScreen.color = color;
    }

    public IEnumerator FadeOut()
    {
        float targetAlpha = 0f;
        Color color = blackScreen.color;
        // Start from fully black if loading fresh
        color.a = 1f;
        blackScreen.color = color;
        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            blackScreen.color = color;
            yield return null;
        }
        color.a = targetAlpha;
        blackScreen.color = color;
    }
}