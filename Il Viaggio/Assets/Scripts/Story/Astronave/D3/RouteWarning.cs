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
                isTriggered = true;

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();

                // missione
                SceneController.CurrentScene.playerUI.RemoveMission("route");
                SceneController.CurrentScene.playerUI.AddMission("radarAlarm", "Scontro imminente", "Devo potenziare i sensori del radar nella direzione dell'asteroide, in modo che il sistema attivi subito l'allarme ed avvisi gli astronauti.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Un asteroide si sta avvicinando e gli astronauti non se ne sono ancora accorti!");
                SceneController.CurrentScene.SpeakToSelf("L'asteroide deve trovarsi in un punto cieco non controllato dal radar...", 4);
                SceneController.CurrentScene.SpeakToSelf("Devo trovare il modo di avvertirli senza farmi scoprire! Manca davvero poco!", 8);

                // attivazione pointable radar di emergenza
                SceneController.CurrentScene.GetComponent<Astronave>().emergencyRadar.gameObject.SetActive(true);
            }
        });
    }
}
