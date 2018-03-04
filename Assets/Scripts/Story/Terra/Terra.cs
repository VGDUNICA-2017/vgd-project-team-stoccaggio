using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour {  

	// Use this for initialization
	void Start () {

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));

        // missioni
        SceneController.CurrentScene.playerUI.AddMission("test", "benvenuto", "ecco il testo");
        SceneController.CurrentScene.playerUI.AddMission("test1", "benvenuto", "ecco il testo dgdgdfg.fdg dfg drgerwg.werg rweghwrth.wrth  wrth.wtrh.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            SceneController.CurrentScene.playerUI.ActiveteModal("prova");
    }
}
