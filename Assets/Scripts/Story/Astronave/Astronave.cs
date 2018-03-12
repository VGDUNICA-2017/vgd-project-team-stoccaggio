using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronave : MonoBehaviour {

	void Start () {
        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("test", "benvenuto", "ecco il testo");
    }
}
