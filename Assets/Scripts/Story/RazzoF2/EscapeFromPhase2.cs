using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeFromPhase2 : MonoBehaviour {

    private bool isActive = false;

	void Start () {

        Pointable p = GetComponent<Pointable>();

        p.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!isActive)
            {
                // disattiva interazione
                p.pointedText = "";
                p.pointedSubText = "";
                p.RefreshText();
                isActive = true;

                // evento storia
                SceneController.CurrentGameObject.GetComponent<RazzoF2>().FinalHatch();
            }
        });
    }
}
