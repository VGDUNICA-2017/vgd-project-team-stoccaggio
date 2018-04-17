using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGathering : MonoBehaviour {

    private Pointable pointable;
    
    // Use this for initialization
    void Start () {
        pointable = GetComponent<Pointable>();

        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (SceneController.CurrentScene.IsEquipped("bottle"))
            {
                SceneController.CurrentScene.RemoveItem("bottle");
                SceneController.CurrentScene.AddItem(new Item("waterBottle", "Bottiglia piena d'acqua"));

                SceneController.CurrentScene.SpeakToSelf("Ho raccolto dell'acqua!");
                
                pointable.pointedText = "";
                pointable.RefreshText();
            }
            else if (!SceneController.CurrentScene.HasItem("waterBottle"))
            {
                SceneController.CurrentScene.SpeakToSelf("Avrei bisogno di una bottiglia!");
            }
        });
    }
}
