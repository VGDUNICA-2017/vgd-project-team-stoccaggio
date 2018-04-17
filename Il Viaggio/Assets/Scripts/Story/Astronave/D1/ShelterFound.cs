using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterFound : MonoBehaviour {

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "player")
        {
            isTriggered = true;
            SceneController.CurrentScene.SpeakToSelf("Questo posto sembra perfetto!");
            SceneController.CurrentScene.playerUI.RemoveMission("hide");

            StartCoroutine(outro());
        }
    }

    private IEnumerator outro()
    {
        yield return new WaitForSeconds(3);

        SceneController.CurrentScene.SpeakToSelf("Meglio riposare...");

        yield return new WaitForSeconds(3);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay();
        });

        yield break;
    }
}
