using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHatch : MonoBehaviour {

	void Start () {

        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            SceneController.CurrentGameObject.GetComponent<RazzoF1>().FinalHatch();
        });
    }
}
