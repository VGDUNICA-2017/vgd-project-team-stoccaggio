using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofFeel : MonoBehaviour
{
    public float baseDistance = 6f;
    public float minDistance = 3f;

    private GameObject player;
    private PlayerController playerController;
    private Checkpoints playerCheckpoint;

    public UnityEngine.UI.Text conversationNpcBox;

    private bool trigger;  //Si attiva se vieni avvistato
    private bool fixTrigger; //Diventa true una volta che il player viene cassato

    public GameObject allarmPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        playerCheckpoint = player.GetComponent<Checkpoints>();
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
                Speak("");
                allarmPoint.gameObject.SetActive(trigger);
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, playerController.isIdle ? minDistance : baseDistance * playerController.soundProduced);
    }

    private void Speak(string text)
    {
        conversationNpcBox.text = text;
    }

    private IEnumerator Cassato()
    {
        Speak("Allarme! C'e' un intruso! Chiamate le guardie!");
        yield return new WaitForSecondsRealtime(4);

        GameObject.FindWithTag("GameController").GetComponent<LoadingController>().LoadScene("Terra/Scene/Terra1");
    }

    private IEnumerator Avvistato()
    {
        Speak("Mi è sembrato di sentire qualcosa...");
        yield return new WaitForSecondsRealtime(2);
        
        trigger = true; //Setto a true per indicare che il player è stato avvistato
    }
}
