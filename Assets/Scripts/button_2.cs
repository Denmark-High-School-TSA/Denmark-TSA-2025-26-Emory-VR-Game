using UnityEngine;
using System.Collections;

public class button_2:MonoBehaviour
{
    public player_movement player_movement_script;
    
    public void play()
    {
        StartCoroutine(player_movement_script.move_and_fade_out());
    }
}