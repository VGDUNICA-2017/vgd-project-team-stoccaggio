using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    private Pointable pointable;
    public EnemySense sense;
    public GameObject medic;

    public AudioSource blow;

    // Use this for initialization
    void Start () {
        pointable = GetComponent<Pointable>();


        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            
            if(SceneController.CurrentScene.IsEquipped("pipe"))
            {
                blow.Play();

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();

                sense.baseDistance = 0;
                sense.minDistance = 0;
                sense.angleVisual = 0;

                medic.GetComponent<Animator>().SetTrigger("kill");

                StartCoroutine(sceneCoroutine());
            }
            else
            {
                SceneController.CurrentScene.SpeakToSelf("Non posso ammazzarlo a mani nude!", 1);
            }

            
            
        });
    }

    private IEnumerator sceneCoroutine()
    {
        yield return new WaitForSeconds(1);

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Ora devo trovare un modo per atterrare da solo!");

        yield return new WaitForSeconds(4);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            // scena atterraggio
            SceneController.CurrentScene.GetComponent<Astronave>().Landing();

        });
    }
}
