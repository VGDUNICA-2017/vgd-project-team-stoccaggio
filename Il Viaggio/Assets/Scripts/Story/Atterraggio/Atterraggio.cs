using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atterraggio : MonoBehaviour {

    private bool copilotSaved;

    void Start () {

        // caricamento dello stato del salvataggio
        copilotSaved = GameController.CurrentController.gameSaveData.copilotSaved;

        StartCoroutine(cutscene());
    }

    private IEnumerator cutscene()
    {
        if (copilotSaved)
        {
            CutsceneController.CurrentScene.NpcSpeak("Copilota", "Bene, ci avviciniamo all'atmosfera di Marte... Attivo gli studi di difesa termica... Ellius appena raggiungeremo i 4000 piedi di altezza dovremmo far virare l'astronave...", 6, 2);
            CutsceneController.CurrentScene.NpcSpeak("Copilota", "Dovremmo posizionarla lungo l'asse orizzontale e nel frattempo rallentare la velocità di discesa... Conosci un punto di Marte in cui risulterebbe più facile atterrare?", 6, 8);
            CutsceneController.CurrentScene.NpcSpeak("Tu", "Conosco la regione adatta, spero che in questi anni non sia cambiata...", 4, 14);
            CutsceneController.CurrentScene.NpcSpeak("Copilota", "Lo stai per scoprire amico, manca davvero poco!", 4, 18);
            CutsceneController.CurrentScene.NpcSpeak("Tu", "Grazie Sam, iniziamo le manovre di atterraggio allora?", 4, 22);
            CutsceneController.CurrentScene.NpcSpeak("Copilota", "Assolutamente!", 3, 26);
        }
        else
        {
            CutsceneController.CurrentScene.SpeakToSelf("Ho sigillato la cabina di pilotaggio... Ora nessuno degli astronauti potrà impedirmi di tornare a casa...", 5, 0);
            CutsceneController.CurrentScene.SpeakToSelf("L'unico problema è che le fasi di atterraggio sono progettate per essere svolte da due operatori... Non potrò mai fare un atterraggio sicuro... Dovrò fare il possibile per sopravvivere all'impatto... ", 8, 5);
            CutsceneController.CurrentScene.SpeakToSelf("C'è la posso fare... Manca davvero poco... Figli miei, amore mio, sto arrivando! ", 4, 13);


        }

        yield return new WaitForSeconds(19);

        CutsceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            GameController.CurrentController.LoadScene("finale");
        });

        yield break;
    }
}
