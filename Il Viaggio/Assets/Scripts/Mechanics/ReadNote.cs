using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNote : MonoBehaviour {

    [TextArea(3, 10)]
    public string richTextnote;

    private Pointable pointable;

    void Start () {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            SceneController.CurrentScene.playerUI.ActivateModal(richTextnote);
        });
	}
}
