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
        pointable.pointedSubText = "aumenta la temperatura";
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
                SceneController.CurrentScene.SpeakToSelf("Quest dovrebbe aiutarmi a non essere visto");
                firstActivation = false;
            }

            // pointable
            pointable.pointedSubText = "diminuisci la temperatura";
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
            pointable.pointedSubText = "aumenta la temperatura";
            pointable.RefreshText();

            // fine vapore
            vaporeon.GetComponent<ParticleSystem>().Stop();
            audioVapore.GetComponent<AudioSource>().Stop();

            // field of feel nemico
            npc.GetComponent<EnemySense>().maxDistanceView *= 2f;
        }
    }
}
