﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazzoF2 : MonoBehaviour {

    public DoorController door;
    public DoorController hatch;
    public GameObject ElevatorControllerOn;
    public GameObject ElevatorControllerOff;
    public Pointable ElevatorPointable;
    public GameObject audioElevator;
    public GameObject audioBlocco;
    public GameObject audioStaccoPropulsore;

    void Start()
    {
        // intro
        SceneController.CurrentScene.Notification("Propulsore 02", 10);

        SceneController.CurrentScene.SpeakToSelf("Ecco l'ultimo propulsore, posso farcela! ");

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("goUp2", "Go up 2.0!", "Il secondo propulsore del razzo raggiungerà a breve la massima potenza e poi si staccherà, è l'ultimo ostacolo prima di raggiungere l'astronave, devo farcela!");
        //SceneController.CurrentScene.playerUI.AddMission("hints", "Hints", "Nel razzo è presente una scala ed un ascensore che permettono l'accesso all'astronave");

        // inizializzazione countdown
        SceneController.CurrentScene.countdowns.Add("rocket", new Countdown());
        SceneController.CurrentScene.countdowns["rocket"].Set(181);
        SceneController.CurrentScene.SetUITimer("rocket");
        SceneController.CurrentScene.countdowns["rocket"].ExpirationHandler += new Countdown.ExpiretionEventHandler(() =>
        {
            audioStaccoPropulsore.GetComponent<AudioSource>().Play();
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

        SceneController.CurrentScene.SpeakToSelf("Mancava davvero poco, non posso crederci, c'è l'ho fatta! ");
        SceneController.CurrentScene.ClearUITimer("rocket");

        // salvataggio
        SaveFileManager.Save(new GameSaveData()
        {
            currentScenePath = "Astronave",
            currentCheckpointID = 0
        });

        // cambio scena
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            StartCoroutine(Transition());            
        });
    }

    public IEnumerator Transition()
    {
        audioStaccoPropulsore.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(5);

        GameController.CurrentController.LoadScene("scenes/Astronave");
    }

    public void PowerOn()
    {
        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("Sembra essersi attivato il generatore di emergenza");

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
        SceneController.CurrentScene.SpeakToSelf("L'ascensore sta salendo");
        audioElevator.GetComponent<AudioSource>().Play();
        StartCoroutine(ElevatorBlock());        
    }

    private IEnumerator ElevatorBlock()
    {
        yield return new WaitForSeconds(5);

        //audio del blocco
        audioElevator.GetComponent<AudioSource>().Stop();
        audioBlocco.GetComponent<AudioSource>().Play();


        // pensieri del personaggio
        SceneController.CurrentScene.SpeakToSelf("L'ascensore si è bloccato, l'energia residua del generatore non era sufficiente! Devo uscire in fretta da qui! ", 7, 0);

        // shake camera aumentato
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.05f;

        // apertura botola
        hatch.SetLock(false);
    }
}
