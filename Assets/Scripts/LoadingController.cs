using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private UnityEngine.UI.Slider loadingBar;

    public void LoadScene(string scenePath)
    {
        // apertura scena di caricamento
        SceneManager.LoadScene("Scenes/Loading");

        // inzia il caricamento asincrono della nuova scena
        StartCoroutine(LoadSceneAsync(scenePath));
    }

    private IEnumerator LoadSceneAsync(string scenePath)
    {
        // attesa per evitare sfarfallii delle scene
        yield return new WaitForSeconds(2);

        // ricerca la barra di caricamento
        if (GameObject.FindGameObjectWithTag("LoadingSlider") != null)
            loadingBar = GameObject.FindGameObjectWithTag("LoadingSlider").GetComponent<UnityEngine.UI.Slider>();

        AsyncOperation async = SceneManager.LoadSceneAsync(scenePath);

        while (!async.isDone)
        {
            // progresso caricamento
            if(loadingBar != null)
                loadingBar.value = Mathf.Clamp01(async.progress / 0.9f);

            yield return null;
        }
    }
}
