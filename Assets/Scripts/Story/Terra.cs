using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour {  

	// Use this for initialization
	void Start () {

        // test
        SceneController.CurrentScene.AddItem(new Item("bottle", "Peracotto"));
        SceneController.CurrentScene.AddItem(new Item("altro", "Altro"));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
