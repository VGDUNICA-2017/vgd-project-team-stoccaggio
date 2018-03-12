using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOn : MonoBehaviour {

    public GameObject lightOn;
    public GameObject lightOff;

    private Pointable pointable;
    private bool isActive = false;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
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

    // Update is called once per frame
    void Update () {
		
	}
}
