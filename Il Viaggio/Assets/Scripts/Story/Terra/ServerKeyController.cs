using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerKeyController : MonoBehaviour {

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
                SceneController.CurrentScene.playerUI.AddMission("power", "Sovraccarico", "La porta della sala server si aprirà solo durante un blackout. Devo trovare il modo di creare un sovraccarico.");
                missionActivated = true;

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();
            }
        });
    }
}
