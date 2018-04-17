using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTimerStarter : MonoBehaviour {

    private bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!started)
        {
            // attivazione singola
            started = true;

            // blocca il timer della corrente se attivo
            if (!SceneController.CurrentScene.countdowns["power"].isExpired())
                SceneController.CurrentScene.countdowns["power"].Set(1);

            // fine missione sovraccarico
            SceneController.CurrentScene.playerUI.RemoveMission("power");

            // nuova missione e lancio razzo
            SceneController.CurrentScene.playerUI.AddMission("takeTime", "Tempo!", "Prendi tempo in qualche modo!");
            SceneController.CurrentGameObject.GetComponent<Terra>().RocketLaunch1();
        }
    }
}
