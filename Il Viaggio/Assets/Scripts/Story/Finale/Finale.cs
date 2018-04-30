using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {

    public GameObject savedScene;
    public GameObject notsavedScene;

	void Start () {
		
        if(GameController.CurrentController.gameSaveData.copilotSaved)
        {
            savedScene.SetActive(true);
            StartCoroutine(endGame("Insieme e con difficoltà siamo riusciti ad atterrare. Siamo tutti sopravvissuti. Non potrò mai ringraziare abbastanza Sam per il suo aiuto...sono finalmente riuscito a riabbracciare la mia famiglia, la mia gente, dopo anni in cui mi credevano morto. Ho potuto riabbracciare i miei figli, l'amore della mia vita: non credo di aver mai vissuto un momento più bello. Offro a Sam e agli altri membri dell'astronave l'aiuto del mio popolo per la ricostruzione della nave. La mia gente, sentendo le calorose parole che rivolgo a Sam, decidono subito di aiutare lui e gli altri terrestri. E vedendo l'aiuto che sto concedendo loro dopo il modo in cui sono stato trattato, anche gli altri astronauti si ricredono, si scusano per il loro comportamento e, anche se con un po' di timore, chiedono il mio aiuto per poter comunicare con il mio popolo. All'inizio questo viaggio doveva rappresentare solo l'esplorazione di un nuovo mondo, diverso ma allo stesso tempo simile al mio. Alla fine si è dimostrato il primo passo verso la nascita di una nuova alleanza."));
        }
        else
        {
            notsavedScene.SetActive(true);
            StartCoroutine(endGame("L'atterraggio si è rivelato essere uno schianto. Tutti gli astronauti sono morti, io sono riuscito a stento a trascinarmi dalla cabina di pilotaggio sulla mia terra. Dopo anni sono finalmente a casa...poco male se vi sono tornato solo per morire. Almeno la mia famiglia saprà che ho fatto tutto il possibile per tornare da loro. E insieme a questo la mia morte avrà anche un altro scopo, mi permetterà di preparare i miei cari e la mia gente alla minaccia che li aspetta a pochi minuti luce dal nostro pianeta.Quando la mia gente troverà il mio corpo, potrà leggere un ultimo messaggio, scritto col mio sangue: Non fidatevi degli umani, se potessero ci ucciderebbero tutti."));
        }
	}

    private IEnumerator endGame(string finalText)
    {
        yield return new WaitForSeconds(12);

        CutsceneController.CurrentScene.playerUI.OpenCinematicPanel(finalText, () => {
            GameController.CurrentController.LoadScene("MainMenu");
        });
    }
}
