using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour {

    public GameObject[] computers;
    public DoorController serverDoor;
    public Pointable cargoButton;

    // cargo
    public GameObject carrello;
    public GameObject audioCargo;
    public GameObject cargoBoxCollider;
    private bool isActive = false;

    void Start () {

        // sovrascrive il salvataggio
        SaveFileManager.Save(new GameSaveData()
        {
            currentScenePath = "Terra",
            currentCheckpointID = 0
        });

        // intro
        SceneController.CurrentScene.Notification("Base spaziale Vandenberg, California", 10);
        SceneController.CurrentScene.SpeakToSelf("Sono arrivato giusto in tempo negli spogliatoi per prendere una delle tute di riserva per la missione", 4);
        SceneController.CurrentScene.SpeakToSelf("C'è ancora molta strada prima dell'astronave, ma potrei non avere un'altra occasione come questa, devo sbrigarmi!", 8);

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("runAway", "Run away!", "L'astronave sta per decollare, devo trovare il modo di salire prima che sia troppo tardi, senza farmi scoprire!");

        // inizializzazione animazioni/effetti
        audioCargo.GetComponent<AudioSource>().Stop();

        // inizializzazione countdowns
        SceneController.CurrentScene.countdowns.Add("power", new Countdown());
        SceneController.CurrentScene.countdowns["power"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            electricPowerBackOn();
        });

        SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
        SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            SceneController.CurrentScene.SpeakToSelf("Il razzo è partito!");
            SceneController.CurrentScene.ClearUITimer("rocket");
            SceneController.CurrentScene.GameOver();
        });

        // inizializzazione cargo
        setupCargoButton();
    }

    #region Computers

    public void ComputersPowerHandler()
    {
        if (computersOn() == 1)
            SceneController.CurrentScene.SpeakToSelf("Il primo macchinario è acceso...");
        else if (computersOn() == 2)
            SceneController.CurrentScene.SpeakToSelf("Ancora uno...");
        else if (computersOn() == 3)
            SceneController.CurrentScene.SpeakToSelf("Mmmh... La corrente utilizzata non è ancora abbastanza per creare un sovraccarico...");
    }

    private int computersOn()
    {
        int computersOnN = 0;

        foreach (GameObject pc in computers)
            if (pc.GetComponent<ComputerLight>().Active() == true)
                computersOnN++;

        return computersOnN;
    }

    #endregion

    #region Corrente elettrica

    public void ElectricPowerOff()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.SetUITimer("power");

        // luci di emergenza
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Light"))
            a.GetComponent<Animator>().SetTrigger("Emergency");

        // reazioni npc
        SceneController.CurrentScene.NpcSpeak("Guardia", "C'è un sovracarico di corrente, controllate i macchinari!");

        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Alcune porte potrebbero essere aperte ora! Devo sbrigarmi!");

        // apertura porta del server
        serverDoor.SetLock(false);

        // start countdown backup corrente
        SceneController.CurrentScene.countdowns["power"].Set(60);
    }

    private void electricPowerBackOn()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.ClearUITimer("power");

        // elimina l'allarme
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Light"))
            a.GetComponent<Animator>().SetTrigger("Safe");

        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Hanno disattivato i macchinari!");

        // chiusura porta del server
        serverDoor.ForceLock();
    }

    #endregion

    #region Lancio razzo

    public void RocketLaunch1()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.SetUITimer("rocket");

        // start countdown
        SceneController.CurrentScene.countdowns["rocket"].Set(21);

        // dialoghi
        SceneController.CurrentScene.NpcSpeak("Scienziato", "C'è un'interruzione delle comunicazioni, deve esserci un problema nella sala server!");
        SceneController.CurrentScene.NpcSpeak("Scienziato", "Il lancio è stato avviato ora non possiamo fermarlo, dobbiamo solo sperare che si attivi la sequenza di lancio di emergenza, richiederà più tempo, che potremmo usare per ripristinare i contatti ed assicurarci che tutto sia in ordine prima della partenza!", 8, 4);
        SceneController.CurrentScene.NpcSpeak("Megafono", "Sequenza di lancio di emergenza attivata", 4, 12);
        SceneController.CurrentScene.NpcSpeak("Scienziato", "Oh, per fortuna si è attivato, forza sbrighiamoci a riattivare le comunicazioni!", 4, 16);
        SceneController.CurrentScene.NpcSpeak("Megafono", "Sequenza di lancio di emergenza attivata", 4, 20);
    }

    public void RocketLaunch2()
    {
        // missione
        SceneController.CurrentScene.playerUI.RemoveMission("takeTime");
        SceneController.CurrentScene.playerUI.AddMission("rocketTime", "Al razzo!", "Non c'è più tempo! Devo trovare un modo per uscire dalla stanza dei server ed arrivare al razzo!");

        // visualizzazione countdown
        SceneController.CurrentScene.SetUITimer("rocket");

        // start countdown
        SceneController.CurrentScene.countdowns["rocket"].Set(61);

        // dialoghi
        SceneController.CurrentScene.SpeakToSelf("Ho guadagnato poco tempo, devo trovare il modo di raggiungere l'astronave, subito!");
        //SceneController.CurrentScene.NpcSpeak("Megafono", "Sequenza di lancio di emergenza attivata!", 10);
        //SceneController.CurrentScene.NpcSpeak("Scienziato", "E' stato attivato il lancio di emergenza!");
        //SceneController.CurrentScene.NpcSpeak("Scienziato", "E' sicuramente successo qualcosa ai server!");
    }

    #endregion

    #region Entrata razzo

    private void setupCargoButton()
    {
        cargoButton.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!isActive)
            {
                // disattiva interazione
                cargoButton.pointedText = "";
                cargoButton.pointedSubText = "In funzione";
                cargoButton.RefreshText();
                isActive = true;

                // stop del countdown
                SceneController.CurrentScene.playerUI.CountdownReset();
                SetupAnimation();
                SceneController.CurrentScene.countdowns["rocket"].Set(100);

                // dialogo
                SceneController.CurrentScene.SpeakToSelf("Questo montacarichi mi dovrebbe portare dentro il primo propulsore del razzo, ce l'ho fatta!");

                // salvataggio
                SaveFileManager.Save(new GameSaveData()
                {
                    currentScenePath = "Razzo fase 1",
                    currentCheckpointID = 0
                });

                // cambio scena
                SceneController.CurrentScene.playerUI.OpenTransition(() => {
                    GameController.CurrentController.LoadScene("Razzo fase 1");
                });
            }
        });
    }

    private void SetupAnimation()
    {
        audioCargo.GetComponent<AudioSource>().Play();
        cargoBoxCollider.SetActive(true);
        carrello.GetComponent<Animator>().SetTrigger("Move");
    }

    #endregion
}
