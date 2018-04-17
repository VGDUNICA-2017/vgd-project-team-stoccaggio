using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atterraggio : MonoBehaviour {

    private bool copilotSaved;

    void Start () {

        // caricamento dello stato del salvataggio
        copilotSaved = GameController.CurrentController.gameSaveData.copilotSaved;

        StartCoroutine(cutscene());
    }

    private IEnumerator cutscene()
    {
        if (copilotSaved)
            CutsceneController.CurrentScene.NpcSpeak("Copilota", "Discorso");
        else
            CutsceneController.CurrentScene.SpeakToSelf("Copilota");

        yield return new WaitForSeconds(4);

        CutsceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            GameController.CurrentController.LoadScene("finale");
        });

        yield break;
    }
}
