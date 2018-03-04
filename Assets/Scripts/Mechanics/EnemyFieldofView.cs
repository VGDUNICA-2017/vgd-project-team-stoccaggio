using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofView : MonoBehaviour
{
    // variabili
    public string npcName;
    public float angleVisual = 90.0f;
    public float maxDistanceView = 35.0f;

    // componenti
    private CapsuleCollider cl;

    // supporto
    private GameObject player;
    private CapsuleCollider playerCollider;
    private Vector3 sourceEye;
    private Vector3 playerHead;

    // allarme
    private float alertTimeout;
    private bool playerSeen;
    private bool playerCaught;
    public float alertTime = 4.0f;
    public GameObject allarmPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerCollider = player.GetComponent<CapsuleCollider>();

        cl = GetComponent<CapsuleCollider>();

        playerSeen = false;
        playerCaught = false;
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
                    if (!playerSeen)
                    {
                        // avvistato per la prima volta
                        playerSeen = true;
                        alertTimeout = Time.time + alertTime;
                        allarmPoint.gameObject.SetActive(true);
                        SceneController.CurrentScene.NpcSpeak(npcName, "Mi è sembrato di vedere qualcosa...");
                    }
                    else
                    {
                        // il player è stato già avvistato
                        if (playerSeen && !playerCaught)
                        {
                            // controlla che non sia stato beccato
                            if(Time.time >= alertTimeout)
                            {
                                // beccato!
                                playerCaught = true;
                                SceneController.CurrentScene.NpcSpeak(npcName, "Allarme! C'e' un intruso! Chiamate le guardie!");
                                StartCoroutine(catchPlayer());
                            }
                        }
                    }
                }
                else
                {
                    // player non più sotto la visuale dell'NPC
                    if (playerSeen)
                    {
                        // il player era stato avvistato
                        playerSeen = false;
                        allarmPoint.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private IEnumerator catchPlayer()
    {
        yield return new WaitForSecondsRealtime(4);

        SceneController.CurrentScene.GameOver();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(sourceEye, maxDistanceView);
        Gizmos.DrawRay(sourceEye, playerHead - sourceEye);
    }
}