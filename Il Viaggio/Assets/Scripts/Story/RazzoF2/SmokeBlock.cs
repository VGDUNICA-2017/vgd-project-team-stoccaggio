using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBlock : MonoBehaviour {

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            // attivazione singola
            triggered = true;

            SceneController.CurrentScene.SpeakToSelf("Qua c'è troppo fumo! Meglio tornare indietro.");
            SceneController.CurrentScene.SpeakToSelf("Devo trovare un altro modo per salire!");
        }
        else
        {
            SceneController.CurrentScene.SpeakToSelf("Devo salire in un altro modo! Il fumo è molto denso...");
        }
    }
}
