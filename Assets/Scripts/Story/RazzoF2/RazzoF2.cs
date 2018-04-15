using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazzoF2 : MonoBehaviour {

    public DoorController door;
    public DoorController hatch;
    public GameObject ElevatorControllerOn;
    public GameObject ElevatorControllerOff;
    public Pointable ElevatorPointable;

    void Start()
    {
        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("test", "benvenuto", "ecco il testo");

        // inizializzazione countdown
        SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
        SceneController.CurrentScene.countdowns["rocket"].Set(181);
        SceneController.CurrentScene.SetUITimer("rocket");
        SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            SceneController.CurrentScene.SpeakToSelf("Il propulsore si è staccato! E' la fine!");
            SceneController.CurrentScene.ClearUITimer("rocket");
            SceneController.CurrentScene.GameOver();
        });

        // shake camera
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.01f;
        SceneController.CurrentScene.GetCameraShake().StartShake();
    }

    public void FinalHatch()
    {
        // shake camera
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.2f;

        SceneController.CurrentScene.SpeakToSelf("Manca davvero poco!");
        SceneController.CurrentScene.ClearUITimer("rocket");

        // cambio scena
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            GameController.CurrentController.LoadScene("scenes/Terra");
        });
    }

    public void PowerOn()
    {
        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Sembra essersi attivato un generatore di emergenza");

        // sblocca la porta dell'ascensore
        door.SetLock(false);

        // aggiorna le luci dell'ascensore
        ElevatorControllerOff.SetActive(false);
        ElevatorControllerOn.SetActive(true);
        ElevatorPointable.pointedText = "Ascensore pronto!";

        // shake camera aumentato
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.03f;

    }

    public void Elevator()
    {
        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("L'ascensore si è bloccato, l'energia non era sufficiente");

        // shake camera aumentato
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.05f;

        // apertura botola
        hatch.SetLock(false);
    }
}
