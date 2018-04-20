using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour {

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
        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Meglio riposare, non so nemmeno se mi nutriranno a sufficienza dato che le loro provviste sono contate.");
        SceneController.CurrentScene.SpeakToSelf("Dopo il colpo con l'asteroide sicuramente non ne avranno abbastanza per il viaggio di ritorno...", 4);

        yield return new WaitForSeconds(8);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().FreedFromCopilot();
        });

        yield break;
    }
}
