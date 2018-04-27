using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController CurrentController;
    public bool debug = false;
    public GameSaveData gameSaveData = null;

    void Start()
    {
        // carica i dati dell'ultimo salvataggio (se presente)
        if (!debug)
            gameSaveData = SaveFileManager.Load();
    }

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

    #region Caricamento-Salvataggio gioco

    public void LoadGameSave()
    {
        // ricarica i dati del salvataggio corrente
        gameSaveData = SaveFileManager.Load();

        if (gameSaveData != null)
            LoadScene(gameSaveData.currentScenePath);
        else
            LoadScene("Terra");
    }

    public void LoadNewGame()
    {
        gameSaveData = null;
        LoadScene("Terra");
    }

    #endregion

    // rende il gameObject permanente e unico
    void Awake()
    {
        if(CurrentController == null)
        {
            CurrentController = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
