using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyForTheAsteroid : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            SceneController.CurrentScene.SpeakToSelf("Spero di essere arrivato in tempo, devo subito fissare il lettino");
            SceneController.CurrentScene.playerUI.RemoveMission("sleep");

            StartCoroutine(outro());
        }
    }

    private IEnumerator outro()
    {
        yield return new WaitForSeconds(3);

        // camera shake
        SceneController.CurrentScene.GetCameraShake().amplitude = 0.1f;
        SceneController.CurrentScene.GetCameraShake().StartShake();

        yield return new WaitForSeconds(2);


        SceneController.CurrentScene.GetComponent<Astronave>().audioExplosion.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1);

        SceneController.CurrentScene.GetCameraShake().amplitude = 0.5f;

        // esplosione
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Light"))
            a.GetComponent<Animator>().SetTrigger("Emergency");


        yield return new WaitForSeconds(2);


        // chiusura
        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay();
        });

        yield break;
    }
}
