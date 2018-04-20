﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour {

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "player")
        {
            isTriggered = true;

            // dialoghi astronauta
            SceneController.CurrentScene.NpcSpeak("Astronauta", "Non ci posso credere!", 4);
            SceneController.CurrentScene.NpcSpeak("Astronauta", "Ragazzi venite immediatamente!", 4, 3);
            SceneController.CurrentScene.SpeakToSelf("Sono spacciato!", 4);

            StartCoroutine(outro());

        }
    }

    private IEnumerator outro()
    {
        yield return new WaitForSeconds(9);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay();
        });

        yield break;
    }
}
