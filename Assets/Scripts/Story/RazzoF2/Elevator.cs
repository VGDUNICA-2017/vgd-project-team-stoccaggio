using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public DoorController door;
    private Pointable pointable;
    private bool isActive = false;

    public GameObject audioButton;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isActive)
            {
                audioButton.GetComponent<AudioSource>().Play();

                isActive = true;

                // forza la chiusura della porta
                door.ForceLock();

                // resetta il testo
                pointable.pointedText = "";
                SceneController.CurrentScene.playerUI.ActionText("", "");

                // esegue l'evento della scena
                SceneController.CurrentGameObject.GetComponent<RazzoF2>().Elevator();
            }
        });
    }
}
