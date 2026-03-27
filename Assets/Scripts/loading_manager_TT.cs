using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loading_manager_TT : MonoBehaviour
{
    public void LoadGame(string game_name)
    {
        StartCoroutine(start_loading(game_name));
    }

    public IEnumerator start_loading(string game_name)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(game_name, LoadSceneMode.Single);
        loading.allowSceneActivation = false;

        while (!loading.isDone)
        {
            if (loading.progress >= 0.9f)
            {
                loading.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    
}