using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerFire : MonoBehaviour {

    public ParticleSystem Explosion;
    public GameObject Phase1;
    public GameObject Phase2;
    public DoorController Door;

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
                    Explosion.Play();
                    Door.ForceLock();
                    
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
        Phase1.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        Phase2.SetActive(true);
        yield break;
    }
}
