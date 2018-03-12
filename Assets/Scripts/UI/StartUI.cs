using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    public Button continueButton;
    public Button startButton;
    public Button exitButton;

    private GameSaveData gameSaveData;
    
    void Start () {

        // carica i dati del salvataggio corrente
        gameSaveData = SaveFileManager.Load();

        // nascondi pulsanti carica se non ci sono salvataggi
        if(gameSaveData == null)
            continueButton.gameObject.SetActive(false);

        // caricamento partita
        continueButton.onClick.AddListener(() => {
            GameController.CurrentController.gameSaveData = gameSaveData;
            SceneManager.LoadScene(gameSaveData.currentScenePath);
        });

        // nuova partita
        startButton.onClick.AddListener(() => {
            GameController.CurrentController.gameSaveData = null;
            GameController.CurrentController.LoadScene("Scenes/Terra");
        });

        // uscita dal gioco
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
