using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

    public DoorController door;     // porta considerata
    public GameObject item;       // oggetti necessari per lo sblocco

    private Pointable pointable;

    private void Start()
    {
        door = door.GetComponent<DoorController>();
        pointable = GetComponent<Pointable>();

        if(door.isLocked)
            pointable.pointedText = "[F] Sblocca porta";

        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (door.isLocked)
            {
                if (hasItem())
                {
                    door.SetLock(false);
                    
                    SceneController.CurrentScene.RemoveItem(item.GetComponent<Collectible>().itemName);
                    
                    SceneController.CurrentScene.SpeakToSelf("Porta sbloccata!");
                    pointable.RefreshText();
                    pointable.pointedText = "[F] Apri";
                    
                }
                else
                {
                    
                    SceneController.CurrentScene.SpeakToSelf("Mi manca ancora qualcosa per sbloccare la porta");
                }
            }
        });
    }

    // check degli oggetti necessari allo sblocco
    private bool hasItem()
    {
        // se l'oggetto non è contenuto nella lista dell'inventario
        if (!SceneController.CurrentScene.IsEquipped(item.GetComponent<Collectible>().itemName))
            return false;


        return true;
    }
}

