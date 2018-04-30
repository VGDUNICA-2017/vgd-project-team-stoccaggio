using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLeakage : MonoBehaviour {

    public GameObject tools;
    public GameObject pipe;

    public GameObject findingObjects;

    private bool isTriggered = false;
    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            // missione non ancora triggerata
            if (!isTriggered)
            {
                // set trigger
                isTriggered = true;

                // missione
                SceneController.CurrentScene.playerUI.AddMission("waterLeakage", "Acquapark", "Devo trovare tutto il materiale necessario per risolvere la perdita d'acqua, vicino al mio rifugio dovrei trovare il necessario.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("La situazione è davvero molto grave... Questo è uno dei condotti principali della nave, se non si risolvesse subito sia l'idroponica che l'intera astronave potrebbero rimanere senz'acqua...", 5, 0);
                SceneController.CurrentScene.SpeakToSelf("Meglio che agisca subito io, se faccio in fretta potrei risolvere la situazione prima che qualcuno si accorga della perdita...", 4, 5);
                SceneController.CurrentScene.SpeakToSelf("Devo trovare un nuovo condotto e gli strumenti per sostituirlo...", 4, 9);

                // attiva strumenti
                tools.SetActive(true);
                pipe.SetActive(true);                
            }
            // missione triggerata ed oggetti raccolti
            else if(SceneController.CurrentScene.HasItem("pipe") && SceneController.CurrentScene.HasItem("tools"))
            {
                // missione completata
                SceneController.CurrentScene.playerUI.RemoveMission("waterLeakage");

                // item rimossi
                SceneController.CurrentScene.RemoveItem("pipe");
                SceneController.CurrentScene.RemoveItem("tools");

                // aggiornamento testo pointable
                pointable.RefreshText("", "");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Tutto risolto, meglio tornare nel rifugio adesso...");

                // attivazione npc inevitabile
                findingObjects.gameObject.SetActive(true);

                // disabilitazione perdita d'acqua
                this.gameObject.SetActive(false);
            }
            // missione triggerata ed oggetti mancanti
            else if (SceneController.CurrentScene.HasItem("pipe"))
            {
                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Per sistemare la situazione mi servono degli strumenti.");
            }
            else if (SceneController.CurrentScene.HasItem("tools"))
            {
                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Gli strumenti che ho sono sufficienti ma mi manca qualche pezzo di ricambio per sistemare la perdita.");
            }
        });
    }
}
