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

        // nascondi pulsanti carica se non ci sono salvataggi
        if(PlayerPrefs.GetString("Scena") == null)
            continueButton.gameObject.SetActive(false);

        // caricamento partita
        continueButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(
                PlayerPrefs.GetString("Scena")
            );
        });

        // nuova partita
        startButton.onClick.AddListener(() =>
        {

            PlayerPrefs.SetString("Scena", "Terra/Scena/Terra1");
            PlayerPrefs.SetFloat("x", 474.0f);
            PlayerPrefs.SetFloat("y", 68.0f);
            PlayerPrefs.SetFloat("z", 322.0f);

            GameObject.FindWithTag("GameController").GetComponent<LoadingController>().LoadScene("Terra/Scene/Terra1");


        });

        // uscita dal gioco
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
