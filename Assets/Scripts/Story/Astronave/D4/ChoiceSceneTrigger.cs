using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour {

    private bool isTriggered = false;
    public GameObject trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.tag == "player")
        {
            isTriggered = true;

            trigger.SetActive(true);

            SceneController.CurrentScene.playerUI.RemoveMission("explore");
            SceneController.CurrentScene.playerUI.AddMission("choice", "A te la scelta!", "Il copilota sta morendo, puoi salvarlo utilizzando l'ultimo rigeneratore di tessuti che ti è rimasto, oppure andartene, conservandoti il rigeneratore per eventuali complicazioni nell'atterraggio.");
        }
    }
}
