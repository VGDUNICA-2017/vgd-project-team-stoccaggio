using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpiantoPiscina : MonoBehaviour {

    public GameObject npc;
    public GameObject vaporeon;
    public GameObject audioButton;
    public GameObject audioVapore;

    private bool active;
    private bool firstActivation = true;

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // inizializzazione
        active = false;
        pointable.pointedText = "[F] Usa impianto";
        pointable.pointedSubText = "<b>aumenta</b> la temperatura";
        vaporeon.GetComponent<ParticleSystem>().Stop();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            active = !active;
            controller(active);
            audioButton.GetComponent<AudioSource>().Play();
        });
    }

    private void controller(bool isActive)
    {
        if (isActive)
        {
            // prima attivazione
            if (firstActivation)
            {
                SceneController.CurrentScene.SpeakToSelf("Questo dovrebbe aiutarmi a non essere visto");
                SceneController.CurrentScene.NpcSpeak("[Guardia]", "Si è acceso l'impianto di riscaldamento... Che strano... Meglio così, iniziavo ad avere un po' di freddo", 6, 2);
                firstActivation = false;
            }

            // pointable
            pointable.pointedSubText = "<b>diminuisci</b> la temperatura";
            pointable.RefreshText();

            // inzio vapore
            vaporeon.GetComponent<ParticleSystem>().Play();
            audioVapore.GetComponent<AudioSource>().Play();

            // field of feel nemico
            npc.GetComponent<EnemySense>().maxDistanceView /= 2f;
        }
        else
        {
            // pointable
            pointable.pointedSubText = "<b>aumenta</b> la temperatura";
            pointable.RefreshText();

            // fine vapore
            vaporeon.GetComponent<ParticleSystem>().Stop();
            audioVapore.GetComponent<AudioSource>().Stop();

            // field of feel nemico
            npc.GetComponent<EnemySense>().maxDistanceView *= 2f;
        }
    }
}
