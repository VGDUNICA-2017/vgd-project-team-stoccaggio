using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirConduitsFound : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            transform.parent.gameObject.SetActive(false);
            SceneController.CurrentScene.SpeakToSelf("Sembrerebbe che l'astronave sia dotata di un ampio condotto d'ossigeno!");
            SceneController.CurrentScene.SpeakToSelf("Potrebbe essere molto utile per rimanere inosservsati...", 3);
        }
    }
}
