using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemName;
    public string itemDisplayName;

    private Pointable pointable;

	void Awake () {

        // componente pointable
        pointable = GetComponent<Pointable>();

        // testo
        pointable.pointedText = "[F] Raccogli";
        pointable.pointedSubText = itemDisplayName;

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            // aggiunta l'oggetto nell'inventario
            SceneController.CurrentScene.AddItem(new Item(itemName, itemDisplayName));

            // sparizione gameobject
            gameObject.SetActive(false);
        });
    }
}
