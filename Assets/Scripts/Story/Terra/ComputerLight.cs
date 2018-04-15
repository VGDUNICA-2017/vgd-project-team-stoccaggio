using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLight : MonoBehaviour {

    public GameObject lightOn;
    public GameObject lightOff;
    public GameObject machineLight;

    private Pointable pointable;
    public GameObject audioButton;
    public GameObject audioPc;
    private bool isActive = false;

	void Start () {

        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!isActive)
            {
                // attivazione audio
                audioButton.GetComponent<AudioSource>().Play();
                audioPc.GetComponent<AudioSource>().Play();

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
