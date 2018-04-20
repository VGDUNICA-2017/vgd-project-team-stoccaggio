using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour {

    public GameObject lightOn;
    public GameObject lightOff;

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
            audioButton.GetComponent<AudioSource>().Play();
            if (!isActive)
            {
                isActive = true;
                lightOff.SetActive(false);
                lightOn.SetActive(true);
                pointable.pointedText = "";
                SceneController.CurrentScene.playerUI.ActionText("", "");
                SceneController.CurrentGameObject.GetComponent<RazzoF2>().PowerOn();
            }
        });
    }
}
