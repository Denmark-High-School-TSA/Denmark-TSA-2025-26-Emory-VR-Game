using UnityEngine;

public class VRButton : MonoBehaviour
{
    public Transform buttonTop;
    public float pressThreshold = 0.02f;
    public FadeScreen fadeScreen;

    private Vector3 startPos;
    private bool pressed = false;

    void Start()
    {
        startPos = buttonTop.localPosition;
    }

    void Update()
    {
        float distance = startPos.y - buttonTop.localPosition.y;

        if (!pressed && distance > pressThreshold)
        {
            pressed = true;
            Press();
        }

        if (pressed && distance < pressThreshold * 0.5f)
        {
            pressed = false;
        }
    }

    void Press()
    {
        Debug.Log("Button Pressed!");
        fadeScreen.FadeIn();
    }
}