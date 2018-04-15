using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCentrifuga : MonoBehaviour {

    public GameObject centrifuga;
    //public GameObject doorLeft;
    //public GameObject doorRight;

    private bool isPowered;

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // inizializzazione
        isPowered = false;
        pointable.pointedSubText = "[F] Attiva la centrifuga";

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isPowered)
            {
                isPowered = true;

                // disattiva interazione
                pointable.pointedSubText = "Corrente disattivata";
                pointable.RefreshText();

                // accende la centrifuga
                centrifuga.gameObject.GetComponent<Animator>().SetTrigger("On");

                // disattiva la corrente
                SceneController.CurrentGameObject.GetComponent<Terra>().ElectricPowerOff();
            }
        });

        // effetto del backup della corrente
        SceneController.CurrentScene.countdowns["power"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            // spegne la centrifuga
            isPowered = false;
            centrifuga.gameObject.GetComponent<Animator>().SetTrigger("Off");
            pointable.pointedSubText = "[F] Attiva la centrifuga";
        });
    }
}
