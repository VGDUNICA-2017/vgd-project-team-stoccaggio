using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLight : MonoBehaviour {

    public GameObject lightOn;
    public GameObject lightOff;
    public GameObject machineLight;

    private Pointable pointable;
    private bool isActive = false;

	void Start () {

        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!isActive)
            {
                // attivazione unica
                isActive = true;

                // luci
                lightOff.SetActive(false);
                lightOn.SetActive(true);
                machineLight.SetActive(true);

                // testo puntato
                pointable.pointedText = "";
                SceneController.CurrentScene.playerUI.ActionText("", "");

                // trigger storia
                SceneController.CurrentGameObject.GetComponent<Terra>().ComputersPowerHandler();
            }
        });
    }

    public bool Active()
    {
        return isActive;
    }
}
