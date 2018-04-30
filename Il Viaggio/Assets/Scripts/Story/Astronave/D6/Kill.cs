using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    private Pointable pointable;
    public EnemySense sense;
    public GameObject medic;

    public GameObject triggerFuga;
    public GameObject triggerFuga2;

    public AudioSource blow;

    // Use this for initialization
    void Start () {
        pointable = GetComponent<Pointable>();


        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            
            if(SceneController.CurrentScene.IsEquipped("pipe"))
            {
                medic.GetComponent<Animator>().SetTrigger("kill");

                pointable.pointedText = "";
                pointable.pointedSubText = "";
                pointable.RefreshText();

                sense.baseDistance = 0;
                sense.minDistance = 0;
                sense.angleVisual = 0;

                triggerFuga.SetActive(false);
                triggerFuga2.SetActive(false);

                blow.Play();

                StartCoroutine(sceneCoroutine());
            }
            else
            {
                SceneController.CurrentScene.SpeakToSelf("Non posso colpirlo mani nude!", 1);
            }

            
            
        });
    }

    private IEnumerator sceneCoroutine()
    {
        yield return new WaitForSeconds(1);

        // pensieri personaggio
        SceneController.CurrentScene.SpeakToSelf("Ora devo trovare il modo di atterrare... Di certo non potrò ricevere il loro aiuto, dovrò farcela da solo... ", 5, 0);

        yield return new WaitForSeconds(6);

        SceneController.CurrentScene.playerUI.OpenTransition(() =>
        {
            // scena atterraggio
            SceneController.CurrentScene.GetComponent<Astronave>().Landing();

        });
    }
}
