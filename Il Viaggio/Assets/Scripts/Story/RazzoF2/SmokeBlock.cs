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

            SceneController.CurrentScene.SpeakToSelf("Il vapore sulla scala è troppo caldo, non posso passare da qui, devo trovare un'altra via! ");
            SceneController.CurrentScene.SpeakToSelf("Devo trovare un altro modo per salire!");
        }
        else
        {
            SceneController.CurrentScene.SpeakToSelf("Devo salire in un altro modo! Il vapore è troppo caldo...");
        }
    }
}
