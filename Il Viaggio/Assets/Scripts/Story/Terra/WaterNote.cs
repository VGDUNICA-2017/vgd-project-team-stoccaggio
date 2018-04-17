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
                SceneController.CurrentScene.playerUI.AddMission("water", "Corto circuito", "Trova un modo di creare un cortocircuito all'interno della sala server.");
                missionActivated = true;
            }
        });
    }
}
