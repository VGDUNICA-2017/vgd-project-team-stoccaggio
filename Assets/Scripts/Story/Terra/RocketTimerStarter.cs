using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTimerStarter : MonoBehaviour {

    private bool started = false;

    private void OnTriggerExit(Collider other)
    {
        if (!started)
        {
            started = true;

            SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
            SceneController.CurrentScene.countdowns["rocket"].Set(180);
            SceneController.CurrentScene.SetUITimer("rocket");

            SceneController.CurrentScene.NpcSpeak("Megafono", "Countdown partenza razzo avviato!");

            SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
            {
                SceneController.CurrentScene.SpeakToSelf("Il razzo è partito!");
                SceneController.CurrentScene.ClearUITimer();
                SceneController.CurrentScene.GameOver();
            });
        }
    }
}
