using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour {

    public GameObject savedScene;
    public GameObject notsavedScene;

	void Start () {
		
        if(GameController.CurrentController.gameSaveData.copilotSaved)
        {
            savedScene.SetActive(true);
        }
        else
        {
            notsavedScene.SetActive(true);
        }
	}
}
