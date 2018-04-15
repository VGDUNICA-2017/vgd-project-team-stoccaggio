using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofFeel : MonoBehaviour
{
    // variabili
    public string npcName;
    public float baseDistance = 6f;
    public float minDistance = 3f;

    private GameObject player;
    private PlayerController playerController;

    public UnityEngine.UI.Text conversationNpcBox;

    private bool trigger;  //Si attiva se vieni avvistato
    private bool fixTrigger; //Diventa true una volta che il player viene cassato

    public GameObject allarmPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        trigger = false;
        fixTrigger = true;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < baseDistance * playerController.soundProduced || distance <= minDistance)
        {
            if(trigger && fixTrigger) //Se il player è stato già avvistato e non ancora cassato
            {
                fixTrigger = false;
                StartCoroutine(Cassato());
            }
            else
            {
                if (fixTrigger) //Se il player non è stato ancora cassato
                {
                    allarmPoint.gameObject.SetActive(true);
                    StartCoroutine(Avvistato());
                }
            }
        }
        else //Se il player non è più sotto la visuale dell'NPC
        {
            if (trigger) //Se il player era stato avvistato
            {
                trigger = false;
                allarmPoint.gameObject.SetActive(trigger);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, playerController.isIdle ? minDistance : baseDistance * playerController.soundProduced);
    }

    private IEnumerator Cassato()
    {
        SceneController.CurrentScene.NpcSpeak(npcName, "Allarme! C'e' un intruso! Chiamate le guardie!");
        yield return new WaitForSecondsRealtime(4);

        SceneController.CurrentScene.GameOver();
    }

    private IEnumerator Avvistato()
    {
        SceneController.CurrentScene.NpcSpeak(npcName, "Mi è sembrato di vedere qualcosa...");
        yield return new WaitForSecondsRealtime(2);
        
        trigger = true; //Setto a true per indicare che il player è stato avvistato
    }
}
