﻿using System.Collections;
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
            // check degli oggetti necessari allo sblocco
            if (SceneController.CurrentScene.HasItem(item[0].GetComponent<Collectible>().itemName)
            && SceneController.CurrentScene.HasItem(item[1].GetComponent<Collectible>().itemName))
            {
                SceneController.CurrentScene.RemoveItem(item[0].GetComponent<Collectible>().itemName);
                SceneController.CurrentScene.RemoveItem(item[1].GetComponent<Collectible>().itemName);

                SceneController.CurrentScene.SpeakToSelf("Queste provviste mi saranno di grande aiuto durante il viaggio...");
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
