using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeFromPhase2 : MonoBehaviour {

	void Start () {
        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            SceneController.CurrentGameObject.GetComponent<RazzoF2>().FinalHatch();
        });
    }
}
