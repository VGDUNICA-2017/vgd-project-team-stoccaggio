using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

    public DoorController door; //porta considerata
    public GameObject[] item; //oggetti necessari a far aprire la porta
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
                if (IsBlocked())
                {
                    door.SetLock(false);
                    
                    for(int i = 0; i < item.Length; i++)
                    {
                        SceneController.CurrentScene.RemoveItem(item[i].GetComponent<Collectible>().itemName);
                    }

                    SceneController.CurrentScene.SpeakToSelf("Porta sbloccata!");
                    pointable.pointedText = "";
                    pointable.RefreshText();
                }
                else
                {
                    SceneController.CurrentScene.SpeakToSelf("Mi manca ancora qualcosa per sbloccare la porta");
                }
            }
        });
    }

    // restituisce true se la porta è bloccata, ovvero il player non ha raccolto tutti gli item per sbloccarla
    private bool IsBlocked()
    {
        int count = 0;
        while (count < item.Length)
        {
            // se l'oggetto non è contenuto nella lista dell'inventario
            if (!SceneController.CurrentScene.HasItem(item[count].GetComponent<Collectible>().itemName))
                return false;

            count++;
        }

        return true;
    }
}

