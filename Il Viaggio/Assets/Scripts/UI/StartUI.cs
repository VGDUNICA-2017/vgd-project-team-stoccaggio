﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour {

    public Button continueButton;
    public Button startButton;
    public Button exitButton;

    void Start () {

        // nascondi pulsanti carica se non ci sono salvataggi
        if(GameController.CurrentController.gameSaveData == null)
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