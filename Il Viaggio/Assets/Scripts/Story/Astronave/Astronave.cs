using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronave : MonoBehaviour {

    public GameObject[] storyDays;

    // D2
    public Pointable water;
    public Pointable food;

    // D3
    public Pointable emergencyRadar;
    public Pointable bed1;
    public Pointable bed2;
    public GameObject bedTrigger;

    // D6
    public GameObject saved;
    public GameObject notsaved;
    public GameObject evasionSaved;
    public GameObject evasionNotSaved;

    public GameObject audioExplosion;

    // scelta
    public bool choiceDone = false;
    private bool copilotSaved;

    void Start () {

        // caricamento dello stato del salvataggio
        copilotSaved = GameController.CurrentController.gameSaveData.copilotSaved;

        switch (GameController.CurrentController.gameSaveData.currentCheckpointID)
        {
            case 1: loadD1(); break;
            case 2: loadD2(); break;
            case 3: loadD3(); break;
            case 4: loadD4(); break;
            case 5: loadD5(); break;
            case 6: loadD6(); break;
            default:
                loadD1();
                GameController.CurrentController.gameSaveData.currentCheckpointID = 1;
                break;
        }

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));
    }

    #region D1

    private void loadD1()
    {
        storyDays[0].SetActive(true);

        StartCoroutine(Mirror());
    }

    private IEnumerator Mirror()
    {
        // disabilita il controllo del giocatore
        SceneController.CurrentScene.player.gameObject.GetComponent<PlayerController>().enabled = false;

        // discorsi del personaggio
        SceneController.CurrentScene.SpeakToSelf("Sembra un sogno, sono sull'astronave, ancora qualche settimana e potrò tornare a casa, ma non devo distrarmi, ancora tante cose possono andare storte... Marte, famiglia mia, sto arrivando... ", 8, 0);

        yield return new WaitForSeconds(8);

        SceneController.CurrentScene.playerUI.OpenTitlePanel(() =>
        {
            // riabilita controllo del personaggio
            SceneController.CurrentScene.player.gameObject.GetComponent<PlayerController>().enabled = true;

            // attivazione gameobjects del nuovo capitolo
            storyDays[1].SetActive(true);

            // notifica giorno
            SceneController.CurrentScene.Notification("Giorno 1", 6);

            // missione
            SceneController.CurrentScene.playerUI.AddMission("hide", "Rifugio", "Devo trovare un rifugio dove poter riposare durante il viaggio, senza che gli astronauti possano scoprirmi!");
        });

        yield return new WaitForSeconds(4);

        // disabilita riflesso dello specchio
        storyDays[0].SetActive(false);
    }

    #endregion

    #region D2

    private void loadD2()
    {
        // attivazione gameobjects
        storyDays[2].SetActive(true);

        // notifica giorno
        SceneController.CurrentScene.Notification("Giorno 2", 6);

        // missione
        SceneController.CurrentScene.playerUI.AddMission("survive", "Sopravvivere", "Devo trovare il deposito delle provviste degli astronauti e recuperare delle provviste.");

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Devo assolutamente trovare delle provviste da portare nel rifugio...durante il viaggio avrò bisogno di alimentarmi ed idratarmi a sufficienza...", 6, 3);

        // setup missione cibo
        water.ActionHandler += () =>
        {
            surviveMissionCheck();
        };

        food.ActionHandler += () =>
        {
            surviveMissionCheck();
        };
    }

    private void surviveMissionCheck()
    {
        if (SceneController.CurrentScene.HasItem("waterBottle") && SceneController.CurrentScene.HasItem("food"))
        {
            // missione
            SceneController.CurrentScene.playerUI.RemoveMission("survive");

            // pensieri personaggio
            SceneController.CurrentScene.SpeakToSelf("Queste provviste dovrebbero bastarmi, devo tornare al rifugio prima di essere scoperto...", 2);

            // missione
            SceneController.CurrentScene.playerUI.AddMission("goBack", "Go back", "Meglio tornare al rifugio prima di essere scoperto.");
        }
    }

    #endregion

    #region D3

    private void loadD3()
    {
        // attivazione gameobjects
        storyDays[3].SetActive(true);

        // notifica giorno
        SceneController.CurrentScene.Notification("Giorno 7", 6);

        // missione
        SceneController.CurrentScene.playerUI.AddMission("route", "La rotta", "Sarà meglio controllare la rotta seguita dall'astronave dalla sala comandi.");

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Sarà meglio controllare la rotta dell'astronave, questa zona dello spazio può riservare molte insidie se non si tiene sempre sotto controllo il radar...", 6, 4);
        SceneController.CurrentScene.SpeakToSelf("Oltretutto il pilota sta utilizzando il pilota automatico da quando ha lasciato l'atmosfera terrestre...", 5, 10);

        // setup lettini
        bed1.ActionHandler += () =>
        {
            // disattiva lettini
            bed2.gameObject.SetActive(false);

            // missione
            SceneController.CurrentScene.playerUI.RemoveMission("bed");
            SceneController.CurrentScene.playerUI.AddMission("sleep", "Prepararsi all'impatto", "Devo tornare al rifugio e prepararmi!");

            // discorsi
            SceneController.CurrentScene.NpcSpeak("Pilota", "Qui è il capitano che parla: un asteroide è sulla nostra rotta! Non faremo in tempo a spostarci dalla sua rotta, prepariamoci tutti ad un forte impatto!", 6, 0);
            SceneController.CurrentScene.NpcSpeak("Pilota", "Questa non è un'esercitazione! Lasciate subito le vostre mansioni e preparatevi all'impatto, subito!!", 6, 6);

            // attiva trigger
            bedTrigger.SetActive(true);
        };

        bed2.ActionHandler += () =>
        {
            // disattiva lettini
            bed1.gameObject.SetActive(false);

            // missione
            SceneController.CurrentScene.playerUI.RemoveMission("bed");
            SceneController.CurrentScene.playerUI.AddMission("sleep", "Prepararsi all'impatto", "Devo tornare al rifugio e prepararmi!");

            // discorsi
            SceneController.CurrentScene.NpcSpeak("Pilota", "Qui è il capitano che parla: un asteroide è sulla nostra rotta! Non faremo in tempo a spostarci dalla sua rotta, prepariamoci tutti ad un forte impatto!", 6, 0);
            SceneController.CurrentScene.NpcSpeak("Pilota", "Questa non è un'esercitazione! Lasciate subito le vostre mansioni e preparatevi all'impatto, subito!!", 6, 6);


            // attiva trigger
            bedTrigger.SetActive(true);
        };
    }

    public void ActivateBeds()
    {
        bed1.gameObject.SetActive(true);
        bed2.gameObject.SetActive(true);
    }

    #endregion

    #region D4

    private void loadD4()
    {
        // attivazione gameobjects
        storyDays[4].SetActive(true);

        SceneController.CurrentScene.SpeakToSelf("L'impatto è stato davvero forte, per fortuna sono ancora tutto intero...chissà quanti danni l'astronave e gli umani avranno ricevuto...sarà meglio controllare...", 6, 3);
    }

    #endregion

    #region D5

    private void loadD5()
    {
        // attivazione gameobjects
        storyDays[5].SetActive(true);

        // notifica giorno
        SceneController.CurrentScene.Notification("Giorno 15", 6);

        // dialoghi
        if(copilotSaved)
        {
            SceneController.CurrentScene.SpeakToSelf("E' passata più di una settimana dall'esplosione, sembra che gli altri astronauti non abbiano creduto alle parole del copilota, per fortuna...forse posso tornare ad essere più tranquillo...", 8, 2);
            SceneController.CurrentScene.SpeakToSelf("Cosa è questo rumore? Sembra acqua, sarà meglio controllare...", 5, 10);
        }
        else
        {
            // dialogo
            SceneController.CurrentScene.playerUI.OpenCinematicPanel(@"
E' passata più di una settimana dall'esplosione, i tre astronauti sopravvissuti sono ancora distrutti dal dolore per la perdita del copilota, in particolare il pilota sembra convinto che qualcuno abbia manomesso il pilota automatico...
In realtà dalle analisi che ho fatto sul computer di bordo il problema sarebbe nato da un difetto di programmazione del sistema...
Forse però risulta più facile per lui pensare che questa perdita sia dovuta ad un atto volontario piuttosto che ad un comportamento negligente...
Posso solo sperare che, nonostante la perdita, riescano a concentrarsi sul proseguimento del viaggio...
            ", () => {
                SceneController.CurrentScene.SpeakToSelf("Cosa è questo rumore? Sembra acqua, sarà meglio controllare...", 6, 0);
            });
        }
    }

    #endregion

    #region D6

    private void loadD6()
    {
        // attivazione gameobjects
        storyDays[6].SetActive(true);

        // notifica giorno
        SceneController.CurrentScene.Notification("Giorno 18", 6);

        // pensieri del giocatore
        //SceneController.CurrentScene.SpeakToSelf("Sembrerebbe una cella arrangiata... Non si aspettavano di certo un intruso!", 1);

        if (!copilotSaved)
        {
            // attiva gameobject
            notsaved.SetActive(true);

            SceneController.CurrentScene.SpeakToSelf("E' incredibile quello che succede appena gli umani trovano qualcosa o qualcuno che non conoscono... Non ho mai visto una cella costruita con tanta velocità... ", 8, 0);
            SceneController.CurrentScene.SpeakToSelf("Famiglia mia, mi dispiace di avervi deluso, ora non credo che potrò tornare a casa... Non posso rischiare che scoprano voi, la nostra gente...", 6, 8);
            SceneController.CurrentScene.SpeakToSelf("No, ma cosa sto pensando! Devo trovare il modo di liberarmi, sono riuscito ad arrivare fino a qui, non posso perdere le speranze adesso! ", 6, 14);
        }
        else
        {
            // attiva gameobject
            saved.SetActive(true);

            SceneController.CurrentScene.SpeakToSelf("E' incredibile quello che succede appena gli umani trovano qualcosa o qualcuno che non conoscono... Non ho mai visto una cella costruita con tanta velocità... ", 8, 0);
            SceneController.CurrentScene.SpeakToSelf("Famiglia mia, mi dispiace di avervi deluso, ora non credo che potrò tornare a casa... Non posso rischiare che scoprano voi, la nostra gente...", 6, 8);
        }
    }

    public void FreedBreakingBars()
    {
        notsaved.SetActive(false);
        evasionNotSaved.SetActive(true);

        SceneController.CurrentScene.playerUI.CloseTransition(() =>
        {
            // notifica giorno
            SceneController.CurrentScene.Notification("Giorno 23", 6);

            // Pensieri del personaggio
            SceneController.CurrentScene.SpeakToSelf("Saranno ormai giorni che ci sto lavorando, ma finalmente la sbarra è pronta a cedere! ", 0);

            SceneController.CurrentScene.SpeakToSelf("Appena sarò libero dovrò stordire il medico, sarebbe troppo rischioso se attivasse gli allarmi dell'astronave o chiamasse il pilota e l'ingegnere...", 6, 4);
            SceneController.CurrentScene.SpeakToSelf("Grazie al rigeneratore di tessuti sono riuscito a riprendermi, mi reggevo a stento in piedi per gli sforzi di questi giorni e le torture subite da questi tre umani... ", 6, 10);

        });
    }

    public void FreedFromCopilot()
    {
        saved.SetActive(false);
        evasionSaved.SetActive(true);

        SceneController.CurrentScene.playerUI.CloseTransition(() =>
        {
            // notifica giorno
            SceneController.CurrentScene.Notification("Giorno 20", 6);

            // dialogo alieno - copilota
            SceneController.CurrentScene.playerUI.OpenCinematicPanel(@"
[Copilota] Quelle iridi rosse, non potrei dimenticarle...sei tu ad avermi salvato vero? 
[Copilota] Ti ringrazio infinitamente, se non fosse stato per te...gli altri non vogliono sentire ragioni...hanno paura di te, hanno paura che tu possa farci del male...come se durante il periodo in cui non sapevamo della tua presenza non avresti potuto farcene se avessi voluto... 
[Copilota] Io non so se riesci a capirmi, ma voglio aiutarti per quanto posso...ho chiuso i  miei compagni di viaggio nelle loro stanze...non possono fermarci per il momento...spero che poi potranno capire il loro errore di giudizio...ora ti libero...  
[Tu] Grazie...
[Copilota] Allora mi capisci, parli la nostra lingua! 
[Tu] Si, dopo anni trascorsi sulla terra, dopo un atterraggio di emergenza in cui la mia navicella è andata distrutta, ho imparato il vostro modo di esprimervi...
[Copilota] E parli la nostra lingua anche meglio di molti miei colleghi...complimenti! Hai detto che la tua nave è andata distrutta? Quindi sei su questa nave per...tornare a casa? 
[Tu] Si...
[Copilota] Sei originario di Marte quindi? 
* Mi devo veramente fidare? Dal suo sguardo sembra essere sincero... *
[Tu] Si...
[Copilota] Allora devo farti uscire subito da qui, siamo in prossimità di Marte, e mi servirà il tuo aiuto per atterrare, le manovre di atterraggio richiedono due persone per essere eseguite correttamente...non sarò abile come il capitano, ma farò il possibile!
[Tu] Grazie...
[Copilota] Grazie a te, per tutto quello che hai fatto per me, e per questa astronave, ho come l'impressione che tu ci abbia aiutato durante tutto il viaggio...a proposito, io sono Samuel, ma gli amici mi chiamano Sam!
[Tu] Io sono Ellius...
[Copilota] E' un piacere conoscerti Ellius...
            ", () => {

                SceneController.CurrentScene.playerUI.OpenTransition(() => {
                    // scena atterraggio
                    Landing();
                });
            });
        });
    }

    #endregion

    #region Landing

    public void Landing()
    {
        // caricamento scena
        GameController.CurrentController.LoadScene("Atterraggio");
    }

    #endregion

    #region NextDay

    public void GoToNextDay(int nextDay, bool copilotSaved)
    {
        // oggetto salvataggio
        GameSaveData gsd = new GameSaveData()
        {
            currentScenePath = "Astronave",
            currentCheckpointID = nextDay,
            copilotSaved = copilotSaved
        };

        // salvataggio
        SaveFileManager.Save(gsd);

        // caricamento scena
        GameController.CurrentController.LoadScene(gsd.currentScenePath);
    }

    public void GoToNextDay(bool copilotSaved)
    {
        GoToNextDay(GameController.CurrentController.gameSaveData.currentCheckpointID + 1, copilotSaved);
    }

    public void GoToNextDay()
    {
        GoToNextDay(GameController.CurrentController.gameSaveData.copilotSaved);
    }

    #endregion
}
