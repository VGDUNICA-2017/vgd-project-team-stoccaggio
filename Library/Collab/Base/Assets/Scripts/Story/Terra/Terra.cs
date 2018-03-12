using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour {

    public GameObject[] computers;
    public DoorController serverDoor;
    public Pointable cargoButton;

    void Start () {

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("test", "benvenuto", "ecco il testo");
        SceneController.CurrentScene.playerUI.AddMission("test1", "benvenuto", "ecco il testo dgdgdfg.fdg dfg drgerwg.werg rweghwrth.wrth  wrth.wtrh.");

        // inizializzazione countdowns
        SceneController.CurrentScene.countdowns.Add("power", new Countdown());
        SceneController.CurrentScene.countdowns["power"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            electricPowerBackOn();
        });

        SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
        SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            electricPowerBackOn();
        });

        // inizializzazioni
        setupCargoButton();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SceneController.CurrentScene.playerUI.ActivateModal("prova");
    }

    #region Computers

    public void ComputersPowerHandler()
    {
        if (computersOn() == 1)
            SceneController.CurrentScene.SpeakToSelf("Il primo macchinario è acceso...");
        else if (computersOn() == 2)
            SceneController.CurrentScene.SpeakToSelf("Ancora uno...");
        else if (computersOn() == 3)
            SceneController.CurrentScene.SpeakToSelf("Mmmh... La corrente non sembra essere ancora abbastanza...");
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
        SceneController.CurrentScene.playerUI.CountdownSet(SceneController.CurrentScene.countdowns["power"]);

        // luci di emergenza
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Light"))
            a.GetComponent<Animator>().SetTrigger("Emergency");

        // reazioni npc
        SceneController.CurrentScene.NpcSpeak("Guardia", "C'è un sovracarico di corrente, controllate i macchinari!");

        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Alcune porte potrebbero essere aperte ora! Devo sbrigarmi!");

        // apertura porta del server
        serverDoor.isLocked = false;

        // start countdown backup corrente
        SceneController.CurrentScene.countdowns["power"].Set(30);
    }

    private void electricPowerBackOn()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.playerUI.CountdownReset();

        // elimina l'allarme
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Light"))
            a.GetComponent<Animator>().SetTrigger("Safe");

        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Hanno disattivato i macchinari!");

        // chiusura porta del server
        serverDoor.ForceLock();

        // attivazione timer di lancio
        RocketLaunch1();
    }

    #endregion

    #region Lancio razzo

    public void RocketLaunch1()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.playerUI.CountdownSet(SceneController.CurrentScene.countdowns["rocket"]);

        // start countdown
        SceneController.CurrentScene.countdowns["rocket"].Set(21);
        SceneController.CurrentScene.playerUI.CountdownSet(SceneController.CurrentScene.countdowns["rocket"]);

        // dialoghi
        SceneController.CurrentScene.NpcSpeak("Scienziato", "Ora che la corrente è tornata siamo pronti!");
        SceneController.CurrentScene.NpcSpeak("Megafono", "Attenzione: sequenza di lancio attivata...", 10);
    }

    public void RocketLaunch2()
    {
        // visualizzazione countdown
        SceneController.CurrentScene.playerUI.CountdownSet(SceneController.CurrentScene.countdowns["rocket"]);

        // start countdown
        SceneController.CurrentScene.countdowns["rocket"].Set(61);
        SceneController.CurrentScene.playerUI.CountdownSet(SceneController.CurrentScene.countdowns["rocket"]);

        // dialoghi
        SceneController.CurrentScene.NpcSpeak("Megafono", "Sequenza di lancio di emergenza attivata!", 10);
        SceneController.CurrentScene.NpcSpeak("Scienziato", "E' stato attivato il lancio di emergenza!");
        SceneController.CurrentScene.NpcSpeak("Scienziato", "Il blackout ha ritardato il lancio!");
    }

    #endregion

    private void setupCargoButton()
    {
        cargoButton.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            // stop del countdown
            SceneController.CurrentScene.playerUI.CountdownReset();
            SceneController.CurrentScene.countdowns["rocket"].Set(100);

            // dialogo
            SceneController.CurrentScene.SpeakToSelf("Questo montacarichi mi porterà direttamente dentro il razzo!");

            // cambio scena
            SceneController.CurrentScene.playerUI.OpenTransition(() =>
            {
                GameController.CurrentController.LoadScene("MainMenu");
            });
        });
    }
}
