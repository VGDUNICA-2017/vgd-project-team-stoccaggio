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
        SceneController.CurrentScene.SpeakToSelf("Meglio riposare...la mia unica speranza per riuscire a fuggire è tenermi in forze...non credo che mi nutriranno a sufficienza, non hanno abbastanza provviste, molte sono andate perse dopo l'impatto con l'asteroide... ", 8, 0);

        yield return new WaitForSeconds(8);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().FreedFromCopilot();
        });

        yield break;
    }
}
