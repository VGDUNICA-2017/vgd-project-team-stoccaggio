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
            if(!SceneController.CurrentGameObject.GetComponent<Astronave>().choiceDone)
            {
                pointable.pointedText = "";
                pointable.RefreshText();
                SceneController.CurrentScene.playerUI.RemoveMission("choice");
                SceneController.CurrentGameObject.GetComponent<Astronave>().choiceDone = true;
                StartCoroutine(outro());
            }
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
        SceneController.CurrentScene.NpcSpeak("Pilota", "Sam, tutto bene? Cosa è successo? Ho sentito una grossa esplosione provenire da qui...", 4, 2);
        SceneController.CurrentScene.NpcSpeak("Copilota", "Stavo controllando la sala dopo l'impatto quando uno dei motori del propulsore è esploso, il fuoco mi ha investito... ho sentito la mia carne bruciare... pensavo di non farcela...", 8, 6);
        SceneController.CurrentScene.NpcSpeak("Copilota", "Poi all'improvviso ho iniziato a sentirmi meglio... e quando ho riaperto gli occhi ho visto due occhi rossi che mi osservavano...", 6, 14);
        SceneController.CurrentScene.NpcSpeak("Copilota", "Penseresti che delle iridi rosse siano spaventose, invece ci ho visto tanto calore, tanta gentilezza... quell'essere mi ha salvato Andrew!", 7, 20);
        SceneController.CurrentScene.SpeakToSelf("No, mi ha visto, sono stato scoperto! Per me è finita!", 4, 27);
        SceneController.CurrentScene.NpcSpeak("Pilota", "Sam, cosa stai farneticando? Qui non c'è nessuno! Probabilmente hai una commozione cerebrale, il medico sarà qui fra poco, stai tranquillo... ti riprenderai... ", 6, 31);
        SceneController.CurrentScene.SpeakToSelf("Devo sperare che pensi di avermi solo immaginato... chissà cosa potrebbero farmi se mi trovassero...", 5, 37);

        yield return new WaitForSeconds(44);

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
        SceneController.CurrentScene.NpcSpeak("Pilota", "Oh no...Sam? SAM? No, non puoi essere morto...", 4, 2);
        SceneController.CurrentScene.NpcSpeak("Pilota", "MAX! DOTTORESSA! No, no, no, no Maxine riuscirà a farti stare meglio, vedrai, ci riuscirà. E' il miglior dottore che abbia conosciuto...ci riuscirà...ci...", 8, 6);
        SceneController.CurrentScene.SpeakToSelf("Mi dispiace...non potevo rischiare di salvarlo...", 6, 14);
        SceneController.CurrentScene.NpcSpeak("Pilota", "Quell'asteroide non avremmo mai dovuto incontrarlo, chi può aver cambiato la rotta, chi...", 7, 20);

        yield return new WaitForSeconds(28);

        // transizione
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay(false);
        });

        yield break;
    }
}