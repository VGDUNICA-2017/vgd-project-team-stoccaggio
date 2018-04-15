using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteWarning : MonoBehaviour {

    private bool isTriggered = false;
    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!isTriggered)
            {
                // missione
                SceneController.CurrentScene.playerUI.RemoveMission("route");
                SceneController.CurrentScene.playerUI.AddMission("radarAlarm", "Scontro imminente!", "Attiva il radar di emergeneza per permettere al sistem di monitoraggio di attivare l'allarme.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Non si sono accorti di un asteroide lungo la rotta!");
                SceneController.CurrentScene.SpeakToSelf("Devo attivare il loro radar di emergenza o non sistemeranno mai la rotta!", 3);

                // attivazione pointable radar di emergenza
                SceneController.CurrentScene.GetComponent<Astronave>().emergencyRadar.gameObject.SetActive(true);
            }
        });
    }
}
