using UnityEngine;
using System.Collections;

public class button_1 : MonoBehaviour
{
    public player_movement player_movement_script;
    public loading_manager loading_manager_script;
    public string game_name;

    public void play()
    {
        StartCoroutine(scene_swap_prep());
    }

    public IEnumerator scene_swap_prep()
    {
        yield return StartCoroutine(player_movement_script.move_and_fade_out());
        StartCoroutine(loading_manager_script.start_loading(game_name));
    }
}