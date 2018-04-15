using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHatch : MonoBehaviour {

    private bool isActive = false;

    void Start()
    {

        Pointable p = GetComponent<Pointable>();

        p.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isActive)
            {
                if (SceneController.CurrentScene.IsEquipped("rocketWrench"))
                {
                    // disattiva interazione
                    p.pointedText = "";
                    p.pointedSubText = "";
                    p.RefreshText();
                    isActive = true;

                    // evento storia
                    SceneController.CurrentGameObject.GetComponent<RazzoF1>().FinalHatch();
                }
                else
                {
                    SceneController.CurrentScene.SpeakToSelf("Non si apre");
                    SceneController.CurrentScene.SpeakToSelf("Devo trovare qualcosa per forzarla!");
                }
            }
        });
    }
}
