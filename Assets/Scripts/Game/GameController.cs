using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    #region Caricamento scene

    public void LoadScene(string scenePath)
    {
        // resetta il time scale
        Time.timeScale = 1;

        // apertura scena di caricamento
        SceneManager.LoadScene("Scenes/Loading");

        // inzia il caricamento asincrono della nuova scena
        StartCoroutine(LoadSceneAsync(scenePath));
    }

    private IEnumerator LoadSceneAsync(string scenePath)
    {
        // barra di caricamento
        UnityEngine.UI.Slider loadingBar = null;

        // attesa per evitare sfarfallii delle scene
        yield return new WaitForSeconds(2);

        // caricamento asincrono scena
        AsyncOperation async = SceneManager.LoadSceneAsync(scenePath);

        while (!async.isDone)
        {
            // progresso caricamento
            if (loadingBar != null)
            {
                loadingBar.value = Mathf.Clamp01(async.progress / 0.9f);
            }
            else if(GameObject.FindGameObjectWithTag("LoadingSlider") != null)
            {
                // ricerca barra di caricamento
                loadingBar = GameObject.FindGameObjectWithTag("LoadingSlider").GetComponent<UnityEngine.UI.Slider>();
                loadingBar.value = Mathf.Clamp01(async.progress / 0.9f);
            }

            yield return null;
        }
    }

    #endregion

    // rende il gameObject permanente
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
