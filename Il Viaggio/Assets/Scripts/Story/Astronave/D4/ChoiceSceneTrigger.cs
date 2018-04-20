using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSceneTrigger : MonoBehaviour {

    private bool isTriggered = false;
    public GameObject triggerDiFuga;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "player")
        {
            isTriggered = true;

            triggerDiFuga.SetActive(true);

            SceneController.CurrentScene.playerUI.RemoveMission("explore");
            SceneController.CurrentScene.playerUI.AddMission("choice", "A te la scelta!", "Il copilota sta morendo, puoi salvarlo utilizzando l'ultimo rigeneratore di tessuti che ti è rimasto, oppure andartene, conservandoti il rigeneratore per eventuali complicazioni nell'atterraggio.");
        }
    }
}
