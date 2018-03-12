using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerLight : MonoBehaviour {

    public GameObject lightOn;
    public GameObject lightOff;

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
                isActive = true;
                lightOff.SetActive(false);
                lightOn.SetActive(true);
                pointable.pointedText = "";
                SceneController.CurrentScene.playerUI.ActionText("", "");
            }
        });
    }

    public bool Active()
    {
        return isActive;
    }
}
