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
                SceneController.CurrentScene.playerUI.AddMission("waterLeakage", "Acquapark", "La perdita nella stanza dell'idroponica rischia di mettere a repentaglio la vita di tutti. Cerca degli strumenti per ripararla.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("La situazione è gravissima, devo trovare qualche tubo di ricambio.");
                SceneController.CurrentScene.SpeakToSelf("Deve esserci qualche strumento vicino al mio rifugio...", 4);
                
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
                SceneController.CurrentScene.SpeakToSelf("Bene! Son riuscito a sistemare tutto, meglio tornare al mio rifugio prima di incontrare qualcuno.");

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
