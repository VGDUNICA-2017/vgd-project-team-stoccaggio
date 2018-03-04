using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // inizializzazione
        
        pointable.pointedText = "[F] Resetta il timer";

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            
            Controller();
        });
    }

    private void Controller()
    {

        pointable.pointedText = "[F] Resetta il timer";
        pointable.RefreshText();
        SceneController.CurrentScene.countdowns["rocket"].Set(90);

    }
}
