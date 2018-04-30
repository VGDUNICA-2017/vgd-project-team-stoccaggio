using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNote : MonoBehaviour {

    private Pointable pointable;

    private bool missionActivated = false;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!missionActivated)
            {
                SceneController.CurrentScene.playerUI.AddMission("water", "Corto circuito", "Devo trovare un modo per entrare nella sala server e creare un corto circuito che possa ritardare il lancio.");
                missionActivated = true;
            }
        });
    }
}
