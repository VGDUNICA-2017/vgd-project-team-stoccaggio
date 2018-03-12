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
                    SceneController.CurrentScene.RemoveItem("waterBottle");
                    pointable.pointedText = "";
                    StartCoroutine(fireEffects());
                    explosion.Play();
                    door.ForceLock();
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
        yield return new WaitForSecondsRealtime(2);
        phase1.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        phase2.SetActive(true);
        yield break;
    }
}
