using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    public Button continueButton;
    public Button startButton;
    public Button exitButton;

    void Start () {

        // mostra il cursore
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // nascondi pulsanti carica se non ci sono salvataggi
        if (GameController.CurrentController.gameSaveData == null || GameController.CurrentController.gameSaveData.currentScenePath == "Terra")
            continueButton.gameObject.SetActive(false);

        // caricamento partita
        continueButton.onClick.AddListener(() => {
            SceneManager.LoadScene(GameController.CurrentController.gameSaveData.currentScenePath);
        });

        // nuova partita
        startButton.onClick.AddListener(() => {
            GameController.CurrentController.LoadNewGame();
        });

        // uscita dal gioco
        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
