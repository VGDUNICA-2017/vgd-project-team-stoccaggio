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

        SceneController.CurrentScene.SpeakToSelf("Cosa sarà successo?");
        SceneController.CurrentScene.playerUI.AddMission("explore", "Esplora", "Controlla cosa è successo nella sala motori.");
    }
}
