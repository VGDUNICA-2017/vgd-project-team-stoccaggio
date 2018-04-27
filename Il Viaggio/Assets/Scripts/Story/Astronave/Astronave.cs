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
        SceneController.CurrentScene.SpeakToSelf("Nessuno dovrà sapere che sono un alieno... Non devo assolutamente farmi vedere!", 7, 0);

        yield return new WaitForSeconds(8);

        SceneController.CurrentScene.playerUI.OpenTitlePanel(() =>
        {
            // riabilita controllo del personaggio
            SceneController.CurrentScene.player.gameObject.GetComponent<PlayerController>().enabled = true;

            // attivazione gameobjects del nuovo capitolo
            storyDays[1].SetActive(true);

            // missione
            SceneController.CurrentScene.playerUI.AddMission("hide", "Rifugio", "Trova un rifugio dove poter dormire e passare il viaggio senza essere scoperto dagli astronauti.");
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

        // missione
        SceneController.CurrentScene.playerUI.AddMission("survive", "Sopravvivenza", "Trova il deposito delle provviste degli astronauti.");

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Devo assolutamente trovare delle provviste per alimentarmi e idratarmi...", 3);

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
            SceneController.CurrentScene.SpeakToSelf("Ok ho raccolto tutto, devo tornare al rifugio prima di esser scoperto", 2);

            // missione
            SceneController.CurrentScene.playerUI.AddMission("goBack", "Go Back", "Deposita le provviste nel rifugio");
        }
    }

    #endregion

    #region D3

    private void loadD3()
    {
        // attivazione gameobjects
        storyDays[3].SetActive(true);

        // missione
        SceneController.CurrentScene.playerUI.AddMission("route", "La rotta", "Controlla la rotta attuale dalla sala comandi.");

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("E' arrivato il momento di controllare la rotta e magari spostarla a mio favore...", 5);
        SceneController.CurrentScene.SpeakToSelf("Spero che il tempo a mia disposizione sia sufficiente...", 9);

        // setup lettini
        bed1.ActionHandler += () =>
        {
            // disattiva lettini
            bed2.gameObject.SetActive(false);

            // missione
            SceneController.CurrentScene.playerUI.RemoveMission("bed");
            SceneController.CurrentScene.playerUI.AddMission("sleep", "Prepararsi all'impatto!", "Torna nel rifugio e preparati all'impatto!");

            // attiva trigger
            bedTrigger.SetActive(true);
        };

        bed2.ActionHandler += () =>
        {
            // disattiva lettini
            bed1.gameObject.SetActive(false);

            // missione
            SceneController.CurrentScene.playerUI.RemoveMission("bed");
            SceneController.CurrentScene.playerUI.AddMission("sleep", "Prepararsi all'impatto!", "Torna nel rifugio e preparati all'impatto!");

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

        SceneController.CurrentScene.SpeakToSelf("Ci saranno stati danni nell'astronave?", 3);
    }

    #endregion

    #region D5

    private void loadD5()
    {
        // attivazione gameobjects
        storyDays[5].SetActive(true);
    }

    #endregion

    #region D6

    private void loadD6()
    {
        // attivazione gameobjects
        storyDays[6].SetActive(true);

        // pensieri del giocatore
        SceneController.CurrentScene.SpeakToSelf("Sembrerebbe una cella arrangiata... Non si aspettavano di certo un intruso!", 1);

        if (!copilotSaved)
        {
            // attiva gameobject
            notsaved.SetActive(true);

            SceneController.CurrentScene.SpeakToSelf("Devo trovare un modo per uscire, anche se non ho molto tempo a disposizione.", 4);
        }
        else
        {
            // attiva gameobject
            saved.SetActive(true);
        }
    }

    public void FreedBreakingBars()
    {
        notsaved.SetActive(false);
        evasionNotSaved.SetActive(true);

        SceneController.CurrentScene.playerUI.CloseTransition(() =>
        {
            // Pensieri del personaggio
            SceneController.CurrentScene.SpeakToSelf("Finalmente! E' arrivato il momento di fuggire!", 1);

            SceneController.CurrentScene.SpeakToSelf("Conviene stordire il medico, prima che mi veda!", 3);

        });
    }

    public void FreedFromCopilot()
    {
        saved.SetActive(false);
        evasionSaved.SetActive(true);

        SceneController.CurrentScene.playerUI.CloseTransition(() =>
        {
            StartCoroutine(freedFromCopilotRoutine());
        });
    }

    private IEnumerator freedFromCopilotRoutine()
    {
        // dialogo alieno - copilota
        SceneController.CurrentScene.NpcSpeak("Copilota", "Sono riuscito a liberarti, gli altri sono momentaneamente chiusi!");

        yield return new WaitForSeconds(4);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            // scena atterraggio
            Landing();
        });

        yield break;
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
