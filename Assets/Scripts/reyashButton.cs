using UnityEngine;
using System.Collections;

public class reyashButton : MonoBehaviour
{
    public FadeToBlackOnly fade_to_black_only_script;
    public loading_manager loading_manager_script;
    public string game_name;

    public void play()
    {
        StartCoroutine(scene_swap_prep());
    }

    public IEnumerator scene_swap_prep()
    {
        yield return StartCoroutine(fade_to_black_only_script.FadeRoutine());
        StartCoroutine(loading_manager_script.start_loading(game_name));
    }
}