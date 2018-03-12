using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour {  

	// Use this for initialization
	void Start () {

        // senza oggetti
        SceneController.CurrentScene.AddItem(new Item("hands", ""));
    }
}
