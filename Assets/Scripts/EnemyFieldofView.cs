using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofView : MonoBehaviour
{
    // variabili
    public float angleVisual = 90.0f;
    public float maxDistanceView = 35.0f;

    // componenti
    private CapsuleCollider cl;

    // supporto
    private GameObject player;
    private CapsuleCollider playerCollider;
    private Vector3 sourceEye;
    private Vector3 playerHead;

    public UnityEngine.UI.Text conversationNpcBox;

    private bool trigger;  //Si attiva se vieni avvistato
    private bool fixTrigger; //Diventa true una volta che il player viene cassato

    public GameObject allarmPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerCollider = player.GetComponent<CapsuleCollider>();

        cl = GetComponent<CapsuleCollider>();

        trigger = false;
        fixTrigger = true;
    }

    private void FixedUpdate()
    {
        RaycastHit raycastBullet;

        // posizione occhi sorgente
        sourceEye = transform.position + Vector3.up * cl.height;

        // posizione testa bersaglio
        playerHead = player.transform.position + Vector3.up * (playerCollider.height / 2.0f);

        // raggio dall'occhio del personaggio alla testa suo bersaglio
        if (Physics.Raycast(sourceEye, playerHead - sourceEye, out raycastBullet, maxDistanceView))
        {
            // controlla se il raycast colpisce direttamente il bersaglio
            if (raycastBullet.collider == playerCollider)
            {
                // controlla l'angolo
                if (Vector3.Angle(transform.forward, playerHead - sourceEye) < angleVisual / 2.0f)
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
                    if(trigger) //Se il player era stato avvistato
                    {
                        trigger = false;
                        Speak("");
                        allarmPoint.gameObject.SetActive(trigger);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(sourceEye, maxDistanceView);
        Gizmos.DrawRay(sourceEye, playerHead - sourceEye);
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
        Speak("Mi è sembrato di vedere qualcosa...");
        yield return new WaitForSecondsRealtime(2);
        
        trigger = true; //Setto a true per indicare che il player è stato avvistato
    }
}