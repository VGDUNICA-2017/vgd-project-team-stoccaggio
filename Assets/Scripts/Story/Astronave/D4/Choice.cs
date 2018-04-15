using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour {

    public bool copilotSaved;
    public GameObject cutscene;

    private Pointable pointable;

    private void Start()
    {
        pointable = GetComponent<Pointable>();

        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            pointable.pointedText = "";
            pointable.RefreshText();
            SceneController.CurrentScene.playerUI.RemoveMission("choice");
            StartCoroutine(outro());
        });
    }

    private IEnumerator outro()
    {
        yield return new WaitForSeconds(1);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            // attivazione cutscene
            cutscene.SetActive(true);

            // spostamento giocatore
            SceneController.CurrentScene.player.transform.position = cutscene.transform.position;

            SceneController.CurrentScene.playerUI.CloseTransition(() =>
            {
                // cutscene con dialoghi
                if(copilotSaved)
                    StartCoroutine(finaloutroSaved());
                else
                    StartCoroutine(finaloutroNotSaved());
            });
        });

        yield break;
    }

    private IEnumerator finaloutroSaved()
    {
        yield return new WaitForSeconds(1);

        // dialoghi
        SceneController.CurrentScene.NpcSpeak("Astronauta1", "Ma come avrà fatto a sopravvivere?", 4);
        SceneController.CurrentScene.NpcSpeak("Astronauta2", "Non ne ho idea... era in condizioni gravissime!", 4, 4);
        SceneController.CurrentScene.NpcSpeak("Astronauta1", "In tutti i casi dovrebbe aiutarci con le riparazioni anziché girovagare per nulla nell'astronave...", 4, 8);

        yield return new WaitForSeconds(13);

        // transizione
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay(true);
        });

        yield break;
    }

    private IEnumerator finaloutroNotSaved()
    {
        yield return new WaitForSeconds(1);

        // dialoghi
        SceneController.CurrentScene.NpcSpeak("Astronauta", "Che disgrazia... ", 4);
        SceneController.CurrentScene.NpcSpeak("Capitano", "Non capisco come possa esser stata deviata la rotta...", 4, 2);

        yield return new WaitForSeconds(7);

        // transizione
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay(false);
        });

        yield break;
    }
}