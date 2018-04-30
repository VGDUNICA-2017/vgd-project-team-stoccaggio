using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosion;
    public GameObject deathrattle;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "player")
        {
            isTriggered = true;

            explosion.GetComponent<AudioSource>().Play();

            StartCoroutine(attend());
        }
    }

    private IEnumerator attend()
    {
        yield return new WaitForSeconds(2);
        deathrattle.GetComponent<AudioSource>().Play();

        SceneController.CurrentScene.SpeakToSelf("Cosa sarà successo? Il rumore sembra provenire dalla sala motori...devo andare subito a controllare!");
        SceneController.CurrentScene.playerUI.AddMission("explore", "L'esplosione", "Devo controllare la sala motori!");
    }
}
