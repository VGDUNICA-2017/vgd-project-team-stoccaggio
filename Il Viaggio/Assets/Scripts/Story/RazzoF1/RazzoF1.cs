using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazzoF1 : MonoBehaviour {

    public GameObject audioStaccoPropulsore;

    void Start()
    {
        // intro
        SceneController.CurrentScene.Notification("Propulsore 01", 10);

        SceneController.CurrentScene.SpeakToSelf("Manca poco tempo prima che il calore del propulsore mi uccida... Ma non posso arrendermi ora!", 7, 0);

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("goUp", "Go up!", "Il primo propulsore del razzo raggiungerà a breve la massima potenza e poi si staccherà, devo raggiungere il secondo propulsore prima che sia troppo tardi!");
        //SceneController.CurrentScene.playerUI.AddMission("hints", "Hints", "Nel razzo è presente una scala ed un ascensore che permettono l'accesso alla seconda parte del razzo");

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
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.02f;
        SceneController.CurrentScene.GetCameraShake().StartShake();
    }

    public void FinalHatch()
    {
        // shake camera
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.04f;

        // shake camera
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.08f;

        SceneController.CurrentScene.SpeakToSelf("Finalmente, mancava davvero poco!");
        SceneController.CurrentScene.ClearUITimer("rocket");

        // salvataggio
        SaveFileManager.Save(new GameSaveData() {
            currentScenePath = "Razzo fase 2",
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

        GameController.CurrentController.LoadScene("Razzo Fase 2");
    }
}
