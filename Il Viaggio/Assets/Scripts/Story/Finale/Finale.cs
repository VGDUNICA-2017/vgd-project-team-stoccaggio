using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {

    public GameObject savedScene;
    public GameObject notsavedScene;

	void Start () {
		
        if(GameController.CurrentController.gameSaveData.copilotSaved)
        {
            savedScene.SetActive(true);
            StartCoroutine(endGame("finale buono"));

        }
        else
        {
            notsavedScene.SetActive(true);
            StartCoroutine(endGame("finale cattivo"));
        }
	}

    private IEnumerator endGame(string finalText)
    {
        yield return new WaitForSeconds(12);

        CutsceneController.CurrentScene.playerUI.OpenCinematicPanel(finalText, () => {
            GameController.CurrentController.LoadScene("MainMenu");
        });
    }
}
