using UnityEngine;
using System.Collections;

public class VRButton : MonoBehaviour
{
    public Transform buttonTop;
    public float pressThreshold = 0.02f;
    public FadeScreen fadeScreen;

    private Vector3 startPos;
    private bool pressed = false;
    private bool isLoading = false; // ADD THIS

    public loading_manager loading_manager_script;
    public string game_name;

    void Start()
    {
        startPos = buttonTop.localPosition;
    }

    void Update()
    {
        float distance = startPos.z - buttonTop.localPosition.z;

        if (!pressed && !isLoading && distance > pressThreshold) // ADD !isLoading
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
        isLoading = true; // ADD THIS
        StartCoroutine(scene_swap_prep());
    }

    public IEnumerator scene_swap_prep()
    {
        yield return StartCoroutine(fadeScreen.FadeIn());
        yield return StartCoroutine(loading_manager_script.start_loading(game_name));
    }
}