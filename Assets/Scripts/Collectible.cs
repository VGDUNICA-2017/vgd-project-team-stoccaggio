using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	void Start () {
        // evento azione
        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().StoryController.playerInventory.AddItem(new Item(gameObject.GetComponent<Pointable>().pointedSubText, gameObject.GetComponent<Pointable>().pointedSubText));
            gameObject.SetActive(false);
        });
    }
}
