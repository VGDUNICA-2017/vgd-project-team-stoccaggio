using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCentrifuga : MonoBehaviour {

    public GameObject centrifuga;
    //public GameObject doorLeft;
    //public GameObject doorRight;
    public Animator doorLeft;
    public Animator doorRight;

    private bool active;

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();


        active = false;
        pointable.pointedSubText = "[F] Disattiva la corrente";

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            
                active = !active;
                Controller(active);
            
            
        });

    }

    // setta l'animazione delle particelle a seconda che l'impianto sia acceso o spento
    void Controller(bool value)
    {
        if (value)
        {
            pointable.pointedSubText = "Corrente disattivata";
            pointable.RefreshText();
            //disattiva corrente
            centrifuga.gameObject.GetComponent<Animator>().StartPlayback();
            // Apre la porta nel corridoio
            doorLeft.SetTrigger("Open"); 
            doorRight.SetTrigger("Open");

            SceneController.CurrentScene.NpcSpeak("Guardia", "E' mancata la corrente, avviate il ripristino");
            SceneController.CurrentScene.countdowns.Add("power", new Countdown());
            SceneController.CurrentScene.countdowns["power"].Set(10);
            SceneController.CurrentScene.countdowns["power"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
            {
                //Riattiva la corrente
                centrifuga.gameObject.GetComponent<Animator>().StopPlayback();
                active = !active;
                pointable.pointedSubText = "[F] Disattiva la corrente";
                SceneController.CurrentScene.SpeakToSelf("Hanno ripristinato la corrente!");
                //Chiude la porta del corridoio
                doorLeft.SetTrigger("Close");
                doorRight.SetTrigger("Close");
            });
        }
    }
}
