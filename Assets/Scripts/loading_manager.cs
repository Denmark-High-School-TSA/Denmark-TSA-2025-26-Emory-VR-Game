using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loading_manager : MonoBehaviour
{

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
