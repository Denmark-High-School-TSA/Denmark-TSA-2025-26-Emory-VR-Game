using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public Image blackScreen;
    public float fadeSpeed = 1.0f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        blackScreen.gameObject.SetActive(true); // enable before fading in
        float targetAlpha = 1f;
        Color color = blackScreen.color;
        color.a = 0f;
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

    public IEnumerator FadeOut()
    {
        blackScreen.gameObject.SetActive(true); // enable before fading out
        float targetAlpha = 0f;
        Color color = blackScreen.color;
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
        blackScreen.gameObject.SetActive(false); // disable when fully clear
    }
}