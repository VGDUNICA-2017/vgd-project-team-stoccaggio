using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyFood : MonoBehaviour {

    private Pointable pointable;
    public GameObject[] item;      

    private void Start()
    {
       pointable = GetComponent<Pointable>();

        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (hasItem())
            {
                SceneController.CurrentScene.RemoveItem(item[0].GetComponent<Collectible>().itemName);
                SceneController.CurrentScene.RemoveItem(item[1].GetComponent<Collectible>().itemName);

                SceneController.CurrentScene.SpeakToSelf("Ho depositato le provviste!");
                pointable.pointedText = "";
                pointable.RefreshText();

                // missione
                SceneController.CurrentScene.playerUI.RemoveMission("goBack");

                StartCoroutine(outro());
            }
            else
            {
                SceneController.CurrentScene.SpeakToSelf("Devo raccogliere altre provviste...");
            }
        });
    }

    // check degli oggetti necessari allo sblocco
    private bool hasItem()
    {
        // se l'oggetto non è contenuto nella lista dell'inventario
        if (!(SceneController.CurrentScene.HasItem(item[0].GetComponent<Collectible>().itemName) && SceneController.CurrentScene.HasItem(item[1].GetComponent<Collectible>().itemName)))
            return false;


        return true;
    }

    private IEnumerator outro()
    {
        yield return new WaitForSeconds(3);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            SceneController.CurrentScene.GetComponent<Astronave>().GoToNextDay();
        });

        yield break;
    }
}
