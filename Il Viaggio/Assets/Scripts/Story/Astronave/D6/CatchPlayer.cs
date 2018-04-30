using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        SceneController.CurrentScene.NpcSpeak("Medico", "Ehi! Dove stai cercando di scappare?");

        SceneController.CurrentScene.GameOver();
    }
}
