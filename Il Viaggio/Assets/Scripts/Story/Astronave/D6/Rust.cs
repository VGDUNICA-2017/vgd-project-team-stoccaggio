using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rust : MonoBehaviour {

    private bool isTriggered = false;
    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isTriggered)
            {
                isTriggered = true;

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();

                StartCoroutine(sceneCoroutine());
            }
        });
    }

    private IEnumerator sceneCoroutine()
    {
        yield return new WaitForSeconds(1);

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Questa sbarra è arrugginita! Potrebbe rappresentare la mia via di fuga...ci vorranno dei giorni per forzarla, ma ci riuscirò...", 6, 0);

        yield return new WaitForSeconds(6);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().FreedBreakingBars();
        });

        yield break;
    }
}
