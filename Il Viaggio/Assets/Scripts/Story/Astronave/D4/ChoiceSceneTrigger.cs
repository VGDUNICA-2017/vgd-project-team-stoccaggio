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

            SceneController.CurrentScene.SpeakToSelf("Ma quello è il copilota! Ha ustioni di terzo grado su più dell'50% del corpo...non potrà mai sopravvivere...a meno che non faccia qualcosa...", 6, 2);
            SceneController.CurrentScene.SpeakToSelf("Questo è l'ultimo oggetto che mi è rimasto della mia gente, un rigeneratore di tessuti...ma se lo usassi su di lui...non potrei più usarlo in futuro per me...cosa dovrei fare?", 10, 8);

            SceneController.CurrentScene.playerUI.RemoveMission("explore");
            SceneController.CurrentScene.playerUI.AddMission("choice", "Ciò che è e ciò che potrebbe essere", "La vita del copilota è nelle mie mani, posso decidere di salvarlo o di andarmene, qualunque sia la scelta ho poco tempo per farla...");
        }
    }
}
