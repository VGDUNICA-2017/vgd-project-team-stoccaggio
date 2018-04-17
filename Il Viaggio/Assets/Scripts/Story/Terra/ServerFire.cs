using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerFire : MonoBehaviour {

    public ParticleSystem explosion;
    public GameObject phase1;
    public GameObject phase2;
    public DoorController door;

    private Pointable pointable;
    private bool triggered = false;


    void Start () {

        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if(!triggered)
            {
                if(SceneController.CurrentScene.IsEquipped("waterBottle"))
                {
                    triggered = true;

                    // bottiglia
                    SceneController.CurrentScene.RemoveItem("waterBottle");

                    // missione
                    SceneController.CurrentScene.playerUI.RemoveMission("water");

                    // pointable
                    pointable.pointedText = "";

                    // effetti
                    StartCoroutine(fireEffects());
                    explosion.Play();
                    GetComponent<AudioSource>().Play();

                    // chiusura porta
                    door.ForceLock();

                    // lancio del razzo
                    SceneController.CurrentGameObject.GetComponent<Terra>().RocketLaunch2();
                }
                else
                {
                    SceneController.CurrentScene.SpeakToSelf("Mi servirebbe dell'acqua...");
                }
            }
        });
	}

    private IEnumerator fireEffects()
    {
        yield return new WaitForSeconds(2);
        phase1.SetActive(true);
        yield return new WaitForSeconds(4);
        phase2.SetActive(true);
        yield break;
    }
}
