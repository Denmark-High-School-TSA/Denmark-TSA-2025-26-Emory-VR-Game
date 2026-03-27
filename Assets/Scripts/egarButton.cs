using UnityEngine;
using System.Collections;

public class egarButton:MonoBehaviour
{
    public FadeToBlack fadeToBlack;
    public loading_manager loading_manager_script;
    public string game_name;

    public void play()
    {
        StartCoroutine(scene_swap_prep());
    }

    public IEnumerator scene_swap_prep()
    {
        yield return StartCoroutine(fadeToBlack.FadeIn());
        StartCoroutine(loading_manager_script.start_loading(game_name));
    }
}
