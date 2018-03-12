using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour {

    // variabili
    public string npcName;
    public float angleVisual = 90f;
    public float maxDistanceView = 35f;
    public float baseDistance = 6f;
    public float minDistance = 3f;
    public float minViewDistance = 4f;

    // componenti
    private CapsuleCollider cl;

    // supporto
    private GameObject player;
    private CapsuleCollider playerCollider;
    private PlayerController playerController;
    private Vector3 sourceEye;
    private Vector3 playerHead;

    // allarme
    private float alertTimeout;
    private bool alert;
    private bool playerCaught;
    public float alertTime = 2.0f;
    public GameObject allarmPoint;
    private float currentDistance = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        playerCollider = player.GetComponent<CapsuleCollider>();

        cl = GetComponent<CapsuleCollider>();
    }

    private bool fieldOfFeel()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < baseDistance * playerController.soundProduced || distance <= minDistance)
            return true;

        return false;
    }

    private bool fieldOfView()
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
                    currentDistance = raycastBullet.distance;
                    return true;
                }
            }
        }

        return false;
    }

    private void FixedUpdate()
    {
        // se il player non è stato ancora beccato
        if (!playerCaught)
        {
            // controlla se il giocatore è stato percepito
            if (fieldOfFeel() || fieldOfView())
            {
                // contrikka che il player non passi proprio davanti alla guardia
                if (currentDistance != 0.0f && currentDistance <= minViewDistance)
                {
                    // beccato!
                    alert = true;
                    playerCaught = true;
                    SceneController.CurrentScene.NpcSpeak(npcName, "Allarme! C'e' un intruso! Chiamate le guardie!");
                    StartCoroutine(catchPlayer());
                }
                else if (!alert)
                {
                    // avvistato per la prima volta
                    alert = true;
                    alertTimeout = Time.time + alertTime;
                    allarmPoint.gameObject.SetActive(true);
                    SceneController.CurrentScene.NpcSpeak(npcName, "Mi è sembrato di percepire qualcosa di strano...");
                }
                else
                {
                    // il player è stato già avvistato
                    if (!playerCaught)
                    {
                        // controlla che non sia stato beccato
                        if (Time.time >= alertTimeout)
                        {
                            // beccato!
                            playerCaught = true;
                            SceneController.CurrentScene.NpcSpeak(npcName, "Allarme! C'e' un intruso! Chiamate le guardie!");
                            StartCoroutine(catchPlayer());
                        }
                    }
                }
            }
            // se l'NPC non è più in stato di allerta (è trascorso il tempo di allerta)
            else if (Time.time > alertTimeout)
            {
                // il player era stato avvistato
                if (alert)
                {
                    // il player non è più percepito
                    alert = false;
                    allarmPoint.gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator catchPlayer()
    {
        yield return new WaitForSecondsRealtime(2);

        SceneController.CurrentScene.GameOver();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(sourceEye, maxDistanceView);
        Gizmos.DrawRay(sourceEye, playerHead - sourceEye);

        Gizmos.color = Color.yellow;
        if(playerController != null)
            Gizmos.DrawSphere(transform.position, playerController.isIdle ? minDistance : baseDistance * playerController.soundProduced);
    }
}
