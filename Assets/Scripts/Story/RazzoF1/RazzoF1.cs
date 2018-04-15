using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazzoF1 : MonoBehaviour {

    void Start()
    {
        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("test", "benvenuto", "ecco il testo");

        // inizializzazione countdown
        SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
        SceneController.CurrentScene.countdowns["rocket"].Set(181);
        SceneController.CurrentScene.SetUITimer("rocket");
        SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            SceneController.CurrentScene.SpeakToSelf("Il propulsore si è staccato! E' la fine!");
            SceneController.CurrentScene.ClearUITimer("rocket");
            SceneController.CurrentScene.GameOver();
        });

    }

    public void FinalHatch()
    {
        if (SceneController.CurrentScene.HasItem("rocketWrench"))
        {
            SceneController.CurrentScene.SpeakToSelf("Finalmente, mancava davvero poco!");
            SceneController.CurrentScene.ClearUITimer("rocket");

            // salvataggio
            SaveFileManager.Save(new GameSaveData() {
                currentScenePath = "Razzo fase 2",
            });

            // cambio scena
            SceneController.CurrentScene.playerUI.OpenTransition(() =>
            {
                GameController.CurrentController.LoadScene("Razzo Fase 2");
            });
        }
        else
        {
            SceneController.CurrentScene.SpeakToSelf("Non si apre");
            SceneController.CurrentScene.SpeakToSelf("Devo trovare qualcosa per forzarla!");
        }
    }
}
