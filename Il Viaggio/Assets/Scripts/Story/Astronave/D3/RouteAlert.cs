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
                SceneController.CurrentScene.playerUI.AddMission("bed", "Sopravvivere all'impatto", "Devo recuperare il lettino dall'infermeria, ridurrà i danni che riceverò dall'impatto con l'asteroide.");

                // pensieri personaggio
                SceneController.CurrentScene.SpeakToSelf("Bene, il sistema si è accorto dell'asteroide");
                SceneController.CurrentScene.SpeakToSelf("Però non c'è più tempo per spostarsi in una rotta del tutto sicura", 3);
                SceneController.CurrentScene.SpeakToSelf("Devo trovare il modo di mettermi al sicuro prima dell'impatto...", 6);
                SceneController.CurrentScene.SpeakToSelf("Forse un oggetto nell'astronave potrebbe assicurarmi la giusta protezione... il lettino nell'infermeria!", 9);

                // attiva lettini
                SceneController.CurrentScene.GetComponent<Astronave>().ActivateBeds();
            }
        });
    }
}
