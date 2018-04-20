using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteAlert : MonoBehaviour {

    public GameObject lightoff;
    public GameObject lighton;
    public AudioSource alarmSound;
    private bool isTriggered = false;
    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isTriggered)
            {
                isTriggered = true;

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();

                // luci
                lightoff.SetActive(false);
                lighton.SetActive(true);

                // suoni
                alarmSound.Play();

                // missione
                SceneController.CurrentScene.playerUI.RemoveMission("radarAlarm");
                SceneController.CurrentScene.playerUI.AddMission("bed", "Messa in sicurezza!", "Cerca un lettino speciale per poterti mettere al sicuro durante l'impatto.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Bene, il radar di emergenza è attivo");
                SceneController.CurrentScene.SpeakToSelf("L'urto sarà molto forte, nel mio rifugio non resisterei!", 3);
                SceneController.CurrentScene.SpeakToSelf("Devo trovare qualcosa per resistere all'urto...", 6);
                SceneController.CurrentScene.SpeakToSelf("Ricordo di un lettino in infermeria che potrebbe fare al caso mio, meglio controllare!", 9);

                // attiva lettini
                SceneController.CurrentScene.GetComponent<Astronave>().ActivateBeds();
            }
        });
    }
}
